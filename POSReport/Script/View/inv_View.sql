-- View: v_rpt_inv_cost_adjustment

-- DROP VIEW v_rpt_inv_cost_adjustment;

CREATE OR REPLACE VIEW v_rpt_inv_cost_adjustment AS 
 SELECT d."Name", c."Reason", c."Status", p."ProductName", p."Attribute", p."Size", c."LoggedTime"::date AS "DateChanged", c."OldCost", c."NewCost", c."AdjustCostDifference", c."StoreCode", p."Resource"::character varying AS "ProductResource", p."ProductCategoryId" AS "CategoryId"
   FROM "base_CostAdjustment" c
   JOIN "base_Product" p ON p."Id" = c."ProductId"
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId";

ALTER TABLE v_rpt_inv_cost_adjustment OWNER TO postgres;
COMMENT ON VIEW v_rpt_inv_cost_adjustment IS '(POSReport) Get all Cost Adjustment';


-- View: v_rpt_inv_product_list

-- DROP VIEW v_rpt_inv_product_list;

CREATE OR REPLACE VIEW v_rpt_inv_product_list AS 
 SELECT d."Name" AS "Category", p."ProductName", p."Attribute", p."Size", p."QuantityOnHand", p."MarginPercent", p."AverageUnitCost", p."RegularPrice", p."AverageUnitCost" * p."QuantityOnHand" AS "Ext Cost", p."RegularPrice" * p."QuantityOnHand" AS "Ext Price", s."StoreCode", p."ProductCategoryId" AS "CategoryId", p."Resource"::character varying AS "ProductResource"
   FROM "base_Product" p
   JOIN "base_ProductStore" s ON p."Id" = s."ProductId"
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId"
  WHERE p."IsPurge" = false
  ORDER BY d."Name", p."ProductName", s."StoreCode";

ALTER TABLE v_rpt_inv_product_list OWNER TO postgres;
COMMENT ON VIEW v_rpt_inv_product_list IS '(POSReport) Get all Product List';


-- View: v_rpt_inv_quantity_adjustment

-- DROP VIEW v_rpt_inv_quantity_adjustment;

CREATE OR REPLACE VIEW v_rpt_inv_quantity_adjustment AS 
 SELECT d."Name", q."Reason", q."Status", p."ProductName", p."Attribute", p."Size", q."LoggedTime"::date AS "DateChanged", q."OldQty", q."NewQty", q."AdjustmentQtyDiff", q."StoreCode", p."Resource"::character varying AS "ProductResource", p."ProductCategoryId" AS "CategoryId"
   FROM "base_QuantityAdjustment" q
   JOIN "base_Product" p ON p."Id" = q."ProductId"
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId";

ALTER TABLE v_rpt_inv_quantity_adjustment OWNER TO postgres;
COMMENT ON VIEW v_rpt_inv_quantity_adjustment IS '(POSReport) Get all Quantity Adjustment
';


-- View: v_rpt_inv_transfer_stock

-- DROP VIEW v_rpt_inv_transfer_stock;

CREATE OR REPLACE VIEW v_rpt_inv_transfer_stock AS 
 SELECT t."TransferNo", t."DateCreated"::date AS "DateCreated", t."Status", t."FromStore", t."ToStore", t."TotalQuantity", t."UserCreated", t."DateApplied"::date AS "DateApplied", t."UserApplied", t."DateReversed"::date AS "DateReversed", t."UserReversed"
   FROM "base_TransferStock" t;

ALTER TABLE v_rpt_inv_transfer_stock OWNER TO postgres;
COMMENT ON VIEW v_rpt_inv_transfer_stock IS '
List all Transfer Stock and filter by StoreCode

Report: TransferHistory';



-- View: v_rpt_inv_transfer_stock_details

-- DROP VIEW v_rpt_inv_transfer_stock_details;

CREATE OR REPLACE VIEW v_rpt_inv_transfer_stock_details AS 
 SELECT t."TransferNo", td."ItemCode", td."ItemName", t."FromStore" AS "StoreCode", td."ItemAtribute", td."ItemSize", td."Quantity", td."BaseUOM", p."AverageUnitCost", td."Amount", p."ProductCategoryId" AS "CategoryId", p."Resource"::character varying AS "ProductResource"
   FROM "base_TransferStock" t
   JOIN "base_TransferStockDetail" td ON t."Id" = td."TransferStockId"
   JOIN "base_Product" p ON p."Resource"::character varying::text = td."ProductResource"::text
  ORDER BY t."TransferNo", td."ItemCode", td."ItemName", t."FromStore";

ALTER TABLE v_rpt_inv_transfer_stock_details OWNER TO postgres;
COMMENT ON VIEW v_rpt_inv_transfer_stock_details IS 'List all Transfer Stock Details 

Report: TransferStockDetails'';
';

