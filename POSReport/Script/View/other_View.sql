-- View: v_configuration

-- DROP VIEW v_configuration;

CREATE OR REPLACE VIEW v_configuration AS 
 SELECT c."CompanyName", (c."Address"::text || ' '::text) || c."City"::text, c."Email", c."Phone", c."Website", c."Logo", c."Fax", c."DefaultLanguage", c."CurrencySymbol", c."DecimalPlaces", c."FomartCurrency", c."ReceiptMessage"
   FROM "base_Configuration" c;

ALTER TABLE v_configuration OWNER TO postgres;


-- View: v_get_all_store_name

-- DROP VIEW v_get_all_store_name;

CREATE OR REPLACE VIEW v_get_all_store_name AS 
 SELECT s."Name"
   FROM "base_Store" s
  ORDER BY s."Id";

ALTER TABLE v_get_all_store_name OWNER TO postgres;


-- View: v_get_email_config

-- DROP VIEW v_get_email_config;

CREATE OR REPLACE VIEW v_get_email_config AS 
 SELECT c."EmailPop3Server", c."EmailPop3Port", c."EmailAccount", c."EmailPassword"
   FROM "base_Configuration" c;

ALTER TABLE v_get_email_config OWNER TO postgres;


-- View: v_rpt_get_all_permission

-- DROP VIEW v_rpt_get_all_permission;

CREATE OR REPLACE VIEW v_rpt_get_all_permission AS 
        (         SELECT p."Type", p."UserResource", gc."Name", p."Code", p."IsView", p."IsPrint", p."Right"
                   FROM "rpt_Permission" p
              JOIN "base_GenericCode" gc ON p."Code"::text = gc."Code"::text AND p."Type" = 2
        UNION 
                 SELECT p."Type", p."UserResource", gr."Name", p."Code", p."IsView", p."IsPrint", p."Right"
                   FROM "rpt_Permission" p
              JOIN "rpt_Group" gr ON p."Code"::text = gr."Code"::text AND p."Type" = 0)
UNION 
         SELECT p."Type", p."UserResource", r."Name", p."Code", p."IsView", p."IsPrint", p."Right"
           FROM "rpt_Permission" p
      JOIN "rpt_Report" r ON p."Code"::text = r."Code"::text AND p."Type" = 1
  ORDER BY 1, 3;

ALTER TABLE v_rpt_get_all_permission OWNER TO postgres;
COMMENT ON VIEW v_rpt_get_all_permission IS '(POSReport)
Get all permission
';



-- View: v_sale_by_product_detail

-- DROP VIEW v_sale_by_product_detail;

CREATE OR REPLACE VIEW v_sale_by_product_detail AS 
 SELECT de."Name" AS "Category", p."Code", p."ProductName", s."StoreCode", p."Attribute", p."Size", s."OrderDate" AS "Order Date", s."SONumber" AS "Order Number", sum(DISTINCT 
        CASE COALESCE(u."BaseUnitNumber", 0::numeric)
            WHEN 0 THEN sh."PackedQty"
            ELSE sh."PackedQty" * u."BaseUnitNumber"
        END) - 
        CASE COALESCE(rt."ReturnQtyUOM", 0::bigint::numeric)
            WHEN 0 THEN 0::bigint::numeric
            ELSE rt."ReturnQtyUOM"
        END AS "Sold Quantity", pu."Name", sum(DISTINCT d."SubTotal") - sum(DISTINCT 
        CASE COALESCE(rt."ReturnQtyUOM", 0::bigint::numeric)
            WHEN 0 THEN 0::numeric
            ELSE rt."ReturnAmount"
        END) AS "Sale Amount"
   FROM "base_SaleOrder" s
   JOIN "base_SaleOrderDetail" d ON d."SaleOrderId" = s."Id"
   JOIN "base_SaleOrderShip" sos ON sos."SaleOrderResource"::uuid = s."Resource" AND sos."IsShipped" = true
   JOIN "base_SaleOrderShipDetail" sh ON sh."SaleOrderDetailResource"::uuid = d."Resource"
   JOIN "base_Product" p ON sh."ProductResource"::uuid = p."Resource"
   JOIN "base_Department" de ON de."Id" = p."ProductCategoryId"
   JOIN "base_ProductStore" st ON p."Id" = st."ProductId"
   LEFT JOIN "base_ProductUOM" u ON u."UOMId" = d."UOMId"
   LEFT JOIN "base_UOM" pu ON pu."Id" = p."BaseUOMId"
   LEFT JOIN ( SELECT rd."OrderDetailResource", rd."ResourceReturnId", rd."ItemName", sum(rd."ReturnQtyUOM") AS "ReturnQtyUOM", sum(DISTINCT rd."Amount") AS "ReturnAmount", p."Resource"
   FROM "base_ResourceReturnDetail" rd
   JOIN "base_Product" p ON rd."ProductResource"::uuid = p."Resource"
   JOIN "base_ProductStore" ps ON ps."StoreCode" = 0 AND p."Id" = ps."ProductId"
  GROUP BY rd."ResourceReturnId", rd."ItemName", rd."OrderDetailResource", p."Resource") rt ON d."Resource" = rt."OrderDetailResource"::uuid AND rt."Resource" = d."ProductResource"::uuid
  GROUP BY de."Name", p."Code", p."ProductName", s."StoreCode", p."Attribute", p."Size", s."OrderDate", s."SONumber", pu."Name", rt."ReturnQtyUOM"
  ORDER BY s."SONumber";

ALTER TABLE v_sale_by_product_detail OWNER TO postgres;



-- View: v_sale_by_product_summary

-- DROP VIEW v_sale_by_product_summary;

CREATE OR REPLACE VIEW v_sale_by_product_summary AS 
 SELECT d."Name", p."Code", p."ProductName", s."StoreCode", p."Attribute", p."Size", s."SoldQuantity", s."TotalSale", s."TotalCOGS", s."SaleProfit", s."PurchasedQuantity", s."PurchasedSubTotal", s."TotalProfit"
   FROM "base_Product" p
   JOIN "base_ProductStore" s ON p."Id" = s."ProductId"
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId"
  WHERE p."IsPurge" = false
  ORDER BY d."Name", p."ProductName", s."StoreCode";

ALTER TABLE v_sale_by_product_summary OWNER TO postgres;



-- View: v_sale_order_operation

-- DROP VIEW v_sale_order_operation;

CREATE OR REPLACE VIEW v_sale_order_operation AS 
 SELECT DISTINCT s."SONumber", s."OrderStatus", (g."LastName"::text || ','::text) || g."FirstName"::text AS "Customer", s."StoreCode", s."OrderDate", ( SELECT sh."ShipDate"
           FROM "base_SaleOrderShip" sh
          WHERE sh."SaleOrderResource"::uuid = s."Resource"
         LIMIT 1) AS "ShipDate", s."Total" AS "Sale Amount"
   FROM "base_SaleOrder" s
   JOIN "base_SaleOrderDetail" d ON d."SaleOrderId" = s."Id"
   JOIN "base_Product" p ON d."ProductResource"::uuid = p."Resource"
   JOIN "base_ProductStore" st ON p."Id" = st."ProductId" AND s."StoreCode" = 0
   JOIN "base_Guest" g ON g."Resource" = s."CustomerResource"::uuid AND s."Mark" = 'SO'::bpchar
  ORDER BY s."SONumber";

ALTER TABLE v_sale_order_operation OWNER TO postgres;



-- View: v_sale_order_summary

-- DROP VIEW v_sale_order_summary;

CREATE OR REPLACE VIEW v_sale_order_summary AS 
 SELECT t."SONumber", g."Resource" AS "CustomerResource", (g."LastName"::text || ','::text) || g."FirstName"::text AS "Customer", t."StoreCode", t."OrderDate", t."OrderStatus", t."SubTotal", t."TaxAmount", t."DiscountAmount", t."Shipping", t."Total", t."Deposit"
   FROM "base_SaleOrder" t
   JOIN "base_Guest" g ON g."Resource" = t."CustomerResource"::uuid
  WHERE t."OrderStatus" <> 8;

ALTER TABLE v_sale_order_summary OWNER TO postgres;



-- View: v_sale_profit_summary

-- DROP VIEW v_sale_profit_summary;

CREATE OR REPLACE VIEW v_sale_profit_summary AS 
 SELECT s."SONumber", s."OrderStatus", g."Resource" AS "CustomerResource", (((g."LastName"::text || ', '::text) || g."FirstName"::text) || ' '::text) || g."MiddleName"::text AS "Customer", s."StoreCode", s."OrderDate", sum(d."SubTotal") AS "Sale Amount", sum(
        CASE COALESCE(u."BaseUnitNumber", 0::numeric)
            WHEN 0 THEN d."PickQty"
            ELSE d."PickQty" * u."BaseUnitNumber"
        END * p."AverageUnitCost") AS "COGS", sum(d."SubTotal") - sum(
        CASE COALESCE(u."BaseUnitNumber", 0::numeric)
            WHEN 0 THEN d."PickQty"
            ELSE d."PickQty" * u."BaseUnitNumber"
        END * p."AverageUnitCost") AS "Gross Profit"
   FROM "base_SaleOrder" s
   JOIN "base_SaleOrderDetail" d ON d."SaleOrderId" = s."Id"
   JOIN "base_Product" p ON d."ProductResource"::uuid = p."Resource"
   JOIN "base_ProductStore" st ON p."Id" = st."ProductId" AND s."StoreCode" = 0
   JOIN "base_Guest" g ON g."Resource" = s."CustomerResource"::uuid AND s."Mark" = 'SO'::bpchar
   LEFT JOIN "base_ProductUOM" u ON u."ProductStoreId" = st."Id" AND u."UOMId" = d."UOMId"
  GROUP BY s."SONumber", s."OrderStatus", g."Resource", (((g."LastName"::text || ', '::text) || g."FirstName"::text) || ' '::text) || g."MiddleName"::text, s."StoreCode", s."OrderDate"
  ORDER BY s."SONumber";

ALTER TABLE v_sale_profit_summary OWNER TO postgres;



-- View: v_summary_with_activiry

-- DROP VIEW v_summary_with_activiry;

CREATE OR REPLACE VIEW v_summary_with_activiry AS 
 SELECT s."StoreCode" AS "Store", d."Name", sum(s."QuantityOnHand") AS "On Hand", sum(s."QuantityOnHand" * p."RegularPrice") AS "Ext Price", sum(s."SoldQuantity") AS "Sold Quantity", sum(s."TotalSale") AS "Sold Price"
   FROM "base_ProductStore" s
   JOIN "base_Product" p ON p."Id" = s."ProductId"
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId" AND d."LevelId" = 1
  GROUP BY d."Name", s."StoreCode"
  ORDER BY d."Name";

ALTER TABLE v_summary_with_activiry OWNER TO postgres;
COMMENT ON VIEW v_summary_with_activiry IS 'List all store with activity by category
';

