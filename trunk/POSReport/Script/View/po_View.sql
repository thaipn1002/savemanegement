-- View: v_rpt_pur_get_po_locked

-- DROP VIEW v_rpt_pur_get_po_locked;

CREATE OR REPLACE VIEW v_rpt_pur_get_po_locked AS 
 SELECT p."PurchaseOrderNo", p."PurchasedDate"::date AS "PurchasedDate", p."Status", g."Company", p."Total", p."StoreCode", p."UserCreated"
   FROM "base_PurchaseOrder" p
   JOIN "base_Guest" g ON g."Resource" = p."VendorResource"::uuid
  WHERE p."IsLocked";

ALTER TABLE v_rpt_pur_get_po_locked OWNER TO postgres;
COMMENT ON VIEW v_rpt_pur_get_po_locked IS '(POS & POSReport) Get all Purchase Order Locked ';


-- View: v_rpt_pur_po_details

-- DROP VIEW v_rpt_pur_po_details;

CREATE OR REPLACE VIEW v_rpt_pur_po_details AS 
 SELECT po."PurchaseOrderNo", pod."ItemName", po."StoreCode", po."PurchasedDate"::date AS "PurchasedDate", po."Status", pod."Quantity", pod."Amount", pod."ProductResource"
   FROM "base_PurchaseOrder" po
   JOIN "base_PurchaseOrderDetail" pod ON po."Id" = pod."PurchaseOrderId"
  ORDER BY pod."ItemName", po."PurchasedDate"::date;

ALTER TABLE v_rpt_pur_po_details OWNER TO postgres;
COMMENT ON VIEW v_rpt_pur_po_details IS '(POSReport) Use for function sp_pur_get_po_details';

-- View: v_rpt_pur_po_summary

-- DROP VIEW v_rpt_pur_po_summary;

CREATE OR REPLACE VIEW v_rpt_pur_po_summary AS 
 SELECT p."PurchaseOrderNo", g."Company", p."StoreCode", p."Status", p."PurchasedDate"::date AS "PurchasedDate", p."ShipDate"::date AS "ShipDate", p."PaymentDueDate"::date AS "PaymentDueDate", p."Total", p."Paid", p."Balance", g."Resource"::character varying AS "VendorResource"
   FROM "base_PurchaseOrder" p
   JOIN "base_Guest" g ON p."VendorResource"::uuid = g."Resource"
  ORDER BY g."Company", p."PurchasedDate"::date;

ALTER TABLE v_rpt_pur_po_summary OWNER TO postgres;
COMMENT ON VIEW v_rpt_pur_po_summary IS '(POSReport) Use for function sp_pur_get_po_summary';

-- View: v_rpt_pur_product_cost

-- DROP VIEW v_rpt_pur_product_cost;

CREATE OR REPLACE VIEW v_rpt_pur_product_cost AS 
 SELECT d."Name" AS "Category", p."ProductName", po."PurchaseOrderNo", po."PurchasedDate"::date AS "PurchasedDate", g."Company", po."StoreCode", pd."Quantity" - COALESCE(rt."ReturnQty", 0::numeric) AS "Qty", pd."Price", (pd."Quantity" - COALESCE(rt."ReturnQty", 0::numeric)) * pd."Price" AS "Total Cost", po."VendorResource", d."Id" AS "CategoryId", pd."ProductResource"
   FROM "base_PurchaseOrder" po
   JOIN "base_PurchaseOrderDetail" pd ON po."Id" = pd."PurchaseOrderId"
   JOIN "base_Product" p ON p."Resource" = pd."ProductResource"::uuid
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId"
   JOIN "base_Guest" g ON g."Resource" = po."VendorResource"::uuid
   LEFT JOIN ( SELECT rd."OrderDetailResource", rd."ReturnQty"
   FROM "base_ResourceReturnDetail" rd
   JOIN "base_Product" p ON rd."ProductResource"::uuid = p."Resource"
   JOIN "base_ProductStore" ps ON p."Id" = ps."ProductId"
  GROUP BY rd."OrderDetailResource", rd."ReturnQty") rt ON pd."Resource" = rt."OrderDetailResource"::uuid
  ORDER BY g."Company", po."PurchasedDate"::date;

ALTER TABLE v_rpt_pur_product_cost OWNER TO postgres;
COMMENT ON VIEW v_rpt_pur_product_cost IS '(POSReport) Use for function sp_pur_get_product_cost';


-- View: v_rpt_pur_vendor_list

-- DROP VIEW v_rpt_pur_vendor_list;

CREATE OR REPLACE VIEW v_rpt_pur_vendor_list AS 
 SELECT g."Company", COALESCE(g."Phone1", g."Phone2") AS "Phone", g."Email", (COALESCE(ga."AddressLine1", ga."AddressLine2")::text || ', '::text) || ga."City"::text AS "Address", ga."StateProvinceId", ga."PostalCode", ga."CountryId", g."Resource"::character varying AS "VendorResource"
   FROM "base_Guest" g
   JOIN "base_GuestAddress" ga ON g."Id" = ga."GuestId"
  WHERE g."Mark" = 'V'::bpchar
  ORDER BY g."Company";

ALTER TABLE v_rpt_pur_vendor_list OWNER TO postgres;
COMMENT ON VIEW v_rpt_pur_vendor_list IS '(POSReport) Use for function sp_pur_get_vendor_list';


-- View: v_rpt_pur_vendor_product_list

-- DROP VIEW v_rpt_pur_vendor_product_list;

CREATE OR REPLACE VIEW v_rpt_pur_vendor_product_list AS 
 SELECT d."Name" AS "Category", p."ProductName", ps."StoreCode", g."Company", p."AverageUnitCost", g."Resource"::character varying AS "VendorResource", d."Id" AS "CategoryId", p."Resource"::character varying AS "ProductResource"
   FROM "base_Product" p
   JOIN "base_Guest" g ON p."VendorId" = g."Id"
   JOIN "base_ProductStore" ps ON p."Id" = ps."ProductId"
   JOIN "base_Department" d ON d."Id" = p."ProductCategoryId"
  WHERE g."Mark" = 'V'::bpchar
  ORDER BY p."ProductName", ps."StoreCode", g."Company";

ALTER TABLE v_rpt_pur_vendor_product_list OWNER TO postgres;
COMMENT ON VIEW v_rpt_pur_vendor_product_list IS '(POSReport) Use for function sp_pur_get_vendor_product_list';



