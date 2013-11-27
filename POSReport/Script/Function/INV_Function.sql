-- Function: sp_inv_get_category_list(integer, integer)

-- DROP FUNCTION sp_inv_get_category_list(integer, integer);

CREATE OR REPLACE FUNCTION sp_inv_get_category_list(IN department_id integer, IN category_id integer)
  RETURNS TABLE("Department" character varying, "Category" character varying, "TaxCode" character, "Margin" numeric, "MarkUp" numeric) AS
$BODY$ 
DECLARE sql text;
BEGIN
	sql = 'SELECT d."Name" AS "Department", c."Name", c."TaxCodeId", c."Margin", c."MarkUp"
				FROM "base_Department" d
					JOIN "base_Department" c ON c."ParentId" = d."Id" AND c."LevelId" = 1
				WHERE d."LevelId" <> 2';		
	IF department_id <> -1 THEN
		sql = sql || ' AND d."Id" = ' || department_id;
	END IF;
	IF category_id <> -1 THEN
		sql = sql || ' AND c."Id" = ' || category_id;
	END IF;
	sql = sql || ' ORDER BY d."Name", c."Name"';
	return QUERY EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_category_list(integer, integer) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_category_list(integer, integer) IS '(POSReport)
List all Category 
rptCategoryList';


-- Function: sp_inv_get_cost_adjustment(integer, integer, character varying, integer, integer, character varying, character varying)

-- DROP FUNCTION sp_inv_get_cost_adjustment(integer, integer, character varying, integer, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_inv_get_cost_adjustment(IN store_code integer, IN category_id integer, IN product_resource character varying, IN status integer, IN reason integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("Cate" character varying, "Rea" smallint, "Stt" smallint, "ProductName" character varying, attribute character varying, size character varying, loggedtime date, oldcost numeric, newcost numeric, diff numeric, "StoreCode" integer, "ProductResource" character varying, "CategoryId" integer) AS
$BODY$ 
DECLARE sql text;
	tem text;
	condition boolean;
BEGIN
	condition = false;	
				
	IF store_code <> -1 THEN
		condition = true;
		sql = ' v."StoreCode" = ' || store_code::text;
	END IF;	

	IF category_id <> -1 THEN
		tem = ' v."CategoryId" = '::text || category_id::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
				
	IF product_resource <> '' THEN		
		tem = ' v."ProductResource" = ''' || product_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF reason <> -1 THEN
		tem = ' v."Reason" = ' || reason::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."Status" = ' || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."DateChanged"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."DateChanged"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."DateChanged"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."DateChanged"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_inv_cost_adjustment';
	ELSE
		sql = 'SELECT * FROM v_rpt_inv_cost_adjustment v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_cost_adjustment(integer, integer, character varying, integer, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_cost_adjustment(integer, integer, character varying, integer, integer, character varying, character varying) IS '(POSReport) - rptCostAdjustment';


-- Function: sp_inv_get_product_list(integer, integer, character varying)

-- DROP FUNCTION sp_inv_get_product_list(integer, integer, character varying);

CREATE OR REPLACE FUNCTION sp_inv_get_product_list(IN store_code integer, IN category_id integer, IN product_resource character varying)
  RETURNS TABLE("CategoryName" character varying, "ProductName" character varying, "Attribute" character varying, "Size" character varying, "QuantityOnHand" numeric, "MarginPercent" numeric, "AverageUnitCost" numeric, "RegularPrice" numeric, "ExtCost" numeric, "ExtPrice" numeric, "StoreName" integer, "CategoryId" integer, "ProductResource" character varying) AS
$BODY$ 
DECLARE sql text;
	tem text;
	condition boolean;
BEGIN
	condition = false;	
				
	IF store_code <> -1 THEN
		condition = true;
		sql = ' v."StoreCode" = ' || store_code::text;
	END IF;	

	IF category_id <> -1 THEN
		tem = ' v."CategoryId" = '::text || category_id::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
				
	IF product_resource <> '' THEN		
		tem = ' v."ProductResource" = ''' || product_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;	

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_inv_product_list';
	ELSE
		sql = 'SELECT * FROM v_rpt_inv_product_list v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_product_list(integer, integer, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_product_list(integer, integer, character varying) IS '(POSReport)
List all Product and filter by StoreCode

rptProductList (Item List)''';


-- Function: sp_inv_get_product_summary_with_activity(integer, integer)

-- DROP FUNCTION sp_inv_get_product_summary_with_activity(integer, integer);

CREATE OR REPLACE FUNCTION sp_inv_get_product_summary_with_activity(IN store_code integer, IN category_id integer)
  RETURNS TABLE("Category" character varying, "StoreCode" integer, "On-Hand" numeric, "ExtPrice" numeric, "SoleQty" numeric, "SoldPrice" numeric) AS
$BODY$ 
DECLARE sql text;
BEGIN
	sql = 'SELECT d."Name", s."StoreCode", sum(s."QuantityOnHand") AS "On Hand", sum(s."QuantityOnHand"::numeric * p."RegularPrice") AS "Ext Price", sum(s."SoldQuantity") AS "Sold Quantity", sum(s."TotalSale") AS "Sold Price"
				 FROM "base_ProductStore" s
					INNER JOIN "base_Product" p ON p."Id" = s."ProductId"	   
					INNER JOIN "base_Department" d ON d."Id" = p."ProductCategoryId" 	
				 WHERE d."LevelId" = 1';
	IF store_code <> -1 THEN
		sql = sql || ' AND s."StoreCode" = ' || store_code;
	END IF;
	IF category_id <> -1 THEN
		sql = sql || 'AND d."Id" =' || category_id;
	END IF;
	sql = sql || 	'GROUP BY d."Name", s."StoreCode"
			ORDER BY d."Name", s."StoreCode"';
	RETURN QUERY EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_product_summary_with_activity(integer, integer) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_product_summary_with_activity(integer, integer) IS '(POSReport)

rptProductSummaryActivity';


-- Function: sp_inv_get_quantity_adjustment(integer, integer, character varying, integer, integer, character varying, character varying)

-- DROP FUNCTION sp_inv_get_quantity_adjustment(integer, integer, character varying, integer, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_inv_get_quantity_adjustment(IN store_code integer, IN category_id integer, IN product_resource character varying, IN status integer, IN reason integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("Cate" character varying, "Rea" smallint, "Stt" smallint, "ProductName" character varying, attribute character varying, size character varying, loggedtime date, oldcost numeric, newcost numeric, diff numeric, "StoreCode" integer, "ProductResource" character varying, "CategoryId" integer) AS
$BODY$ 

DECLARE sql text;
	tem text;
	condition boolean;
BEGIN
	condition = false;	
				
	IF store_code <> -1 THEN
		condition = true;
		sql = ' v."StoreCode" = ' || store_code::text;
	END IF;	

	IF category_id <> -1 THEN
		tem = ' v."CategoryId" = '::text || category_id::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
				
	IF product_resource <> '' THEN		
		tem = ' v."ProductResource" = ''' || product_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF reason <> -1 THEN
		tem = ' v."Reason" = ' || reason::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."Status" = ' || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;		

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."DateChanged"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."DateChanged"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."DateChanged"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."DateChanged"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_inv_quantity_adjustment';
	ELSE
		sql = 'SELECT * FROM v_rpt_inv_quantity_adjustment v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_quantity_adjustment(integer, integer, character varying, integer, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_quantity_adjustment(integer, integer, character varying, integer, integer, character varying, character varying) IS '(POSReport)

List all Quantity Ajustment and filter  by StoreCode

rptQuantityAdjustment''
';


-- Function: sp_inv_get_reorder_stock(integer, character varying)

-- DROP FUNCTION sp_inv_get_reorder_stock(integer, character varying);

CREATE OR REPLACE FUNCTION sp_inv_get_reorder_stock(IN store_code integer, IN product_resource character varying)
  RETURNS TABLE("Code" character varying, "ProductName" character varying, "Attribute" character varying, "Size" character varying, "QuantityOnHand" numeric, "QuantityAvailable" numeric, "QuantityOnOrder" numeric, "ReorderPoint" numeric, "ReOrderQty" numeric, "Vendor" character varying) AS
$BODY$ 
BEGIN
	IF store_code <> -1 THEN
		IF product_resource <> '' THEN
			RETURN QUERY (
				  SELECT DISTINCT p."Code", p."ProductName", p."Attribute", p."Size", s."QuantityOnHand", s."QuantityAvailable", s."QuantityOnOrder", s."ReorderPoint", 
					(s."ReorderPoint" - (s."QuantityAvailable" + s."QuantityOnOrder")) AS "ReOrderQty", g."Company"--, s."StoreCode"
				   FROM "base_Product" p 
				   INNER JOIN "base_ProductStore" s ON p."Id" = s."ProductId" AND s."StoreCode" = store_code
				   INNER JOIN "base_Guest" g on g."Id" = p."VendorId" AND (lower(g."Mark") = 'v')
				   WHERE s."ReorderPoint" > 0 AND (s."QuantityOnOrder" + s."QuantityAvailable") < s."ReorderPoint"
					AND CAST(p."Resource" AS character varying) = product_resource 
				   ORDER BY p."ProductName"
			  );
		ELSE
			RETURN QUERY (
				  SELECT DISTINCT p."Code", p."ProductName", p."Attribute", p."Size", s."QuantityOnHand", s."QuantityAvailable", s."QuantityOnOrder", s."ReorderPoint", 
					(s."ReorderPoint" - (s."QuantityAvailable" + s."QuantityOnOrder")) AS "ReOrderQty", g."Company"--, s."StoreCode"
				   FROM "base_Product" p 
				   INNER JOIN "base_ProductStore" s ON p."Id" = s."ProductId" AND s."StoreCode" = store_code
				   INNER JOIN "base_Guest" g on g."Id" = p."VendorId" AND (lower(g."Mark") = 'v')
				   WHERE s."ReorderPoint" > 0 AND (s."QuantityOnOrder" + s."QuantityAvailable") < s."ReorderPoint"
				   ORDER BY p."ProductName"
			  );
		END IF;	  
	ELSE  	
		IF product_resource <> '' THEN
			RETURN QUERY (
				   SELECT DISTINCT p."Code", p."ProductName", p."Attribute", p."Size", p."QuantityOnHand", p."QuantityAvailable", p."QuantityOnOrder", p."CompanyReOrderPoint", 
					(p."CompanyReOrderPoint" - (p."QuantityAvailable" + p."QuantityOnOrder")) AS "ReOrderQty", g."Company" --, s."StoreCode"
				   FROM "base_Product" p 
				   RIGHT JOIN "base_ProductStore" s ON p."Id" = s."ProductId"
				   INNER JOIN "base_Guest" g on g."Id" = p."VendorId" AND (lower(g."Mark") = 'v')
				   WHERE p."CompanyReOrderPoint" > 0 AND (p."QuantityAvailable" + p."QuantityOnOrder") < p."CompanyReOrderPoint" 
					AND CAST(p."Resource" AS character varying) = product_resource
				   ORDER BY p."ProductName"
			);
		ELSE	
			RETURN QUERY (
				   SELECT DISTINCT p."Code", p."ProductName", p."Attribute", p."Size", p."QuantityOnHand", p."QuantityAvailable", p."QuantityOnOrder", p."CompanyReOrderPoint", 
					(p."CompanyReOrderPoint" - (p."QuantityAvailable" + p."QuantityOnOrder")) AS "ReOrderQty", g."Company" --, s."StoreCode"
				   FROM "base_Product" p 
				   RIGHT JOIN "base_ProductStore" s ON p."Id" = s."ProductId"
				   INNER JOIN "base_Guest" g on g."Id" = p."VendorId" AND (lower(g."Mark") = 'v')
				   WHERE p."CompanyReOrderPoint" > 0 AND (p."QuantityAvailable" + p."QuantityOnOrder") < p."CompanyReOrderPoint"
				   ORDER BY p."ProductName"
			);
		END IF;	
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_reorder_stock(integer, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_reorder_stock(integer, character varying) IS '(POSReport)

List all  ReOrder Stock and filter by StoreCode

Report: ReOrderStock
';


-- Function: sp_inv_get_transfer_stock(integer, integer, integer, character varying, character varying)

-- DROP FUNCTION sp_inv_get_transfer_stock(integer, integer, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_inv_get_transfer_stock(IN from_store integer, IN to_store integer, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("TransferNo" character varying, "DateCreated" date, "Status" smallint, "FromStore" smallint, "ToStore" smallint, "TotalQuantity" numeric, "UserCreated" character varying, "DateApplied" date, "UserApplied" character varying, "DateReversed" date, "UserReversed" character varying) AS
$BODY$
DECLARE sql text;
	tem text;
	condition boolean;
BEGIN
	condition = false;	
				
	IF from_store <> -1 THEN
		condition = true;
		sql = ' v."FromStore" = ' || from_store::text;
	END IF;
	
	IF to_store <> -1 THEN		
		tem = ' v."ToStore" = ' || to_store::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF status <> -1 THEN
		tem = ' v."Status" = ' || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."DateCreated"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."DateCreated"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."DateCreated"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."DateCreated"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_inv_transfer_stock';
	ELSE
		sql = 'SELECT * FROM v_rpt_inv_transfer_stock v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_transfer_stock(integer, integer, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_transfer_stock(integer, integer, integer, character varying, character varying) IS '(POSReport)
(Report) TransferStockSummary';

-- Function: sp_inv_get_transfer_stock_details(integer, integer, character varying)

-- DROP FUNCTION sp_inv_get_transfer_stock_details(integer, integer, character varying);

CREATE OR REPLACE FUNCTION sp_inv_get_transfer_stock_details(IN store_code integer, IN category_id integer, IN product_resource character varying)
  RETURNS TABLE("TransferNo" character varying, "ItemCode" character varying, "ItemName" character varying, "FromStore" smallint, "Attribute" character varying, "ItemSize" character varying, "Quantity" numeric, "BaseUOM" character varying, "Price" numeric, "Amount" numeric, "CategoryId" integer, "ProductResource" character varying) AS
$BODY$
DECLARE sql text;
	tem text;
	condition boolean;
BEGIN
	condition = false;	
				
	IF store_code <> -1 THEN
		condition = true;
		sql = ' v."StoreCode" = ' || store_code::text;
	END IF;

	IF category_id <> -1 THEN
		tem = ' v."CategoryId" = '::text || category_id::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
				
	IF product_resource <> '' THEN		
		tem = ' v."ProductResource" = ''' || product_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_inv_transfer_stock_details';
	ELSE
		sql = 'SELECT * FROM v_rpt_inv_transfer_stock_details v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_inv_get_transfer_stock_details(integer, integer, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_inv_get_transfer_stock_details(integer, integer, character varying) IS '(POSReport)

List all Transfer Stock history details and filter by StoreCode

Report: TransferStockDetails';


