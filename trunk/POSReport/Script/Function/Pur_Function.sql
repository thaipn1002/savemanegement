-- Function: sp_pur_get_po_details(integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_pur_get_po_details(integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_pur_get_po_details(IN store_code integer, IN product_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("PONumber" character varying, "Product Name" character varying, "StoreCode" integer, "Purchase Date" date, "Status" smallint, "Qty" numeric, "Amount" numeric, "ProductResource" character varying) AS
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
	
	IF product_resource <> '' THEN		
		tem = ' v."ProductResource" = ''' || product_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."Status" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."PurchasedDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."PurchasedDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."PurchasedDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."PurchasedDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
		

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_pur_po_details';
	ELSE
		sql = 'SELECT * FROM v_rpt_pur_po_details v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pur_get_po_details(integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pur_get_po_details(integer, character varying, integer, character varying, character varying) IS '(POSReport)
Get Purchase Order Details

rptPODetails';


-- Function: sp_pur_get_po_summary(integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_pur_get_po_summary(integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_pur_get_po_summary(IN store_code integer, IN vendor_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("PONumber" character varying, "Company" character varying, "StoreCode" integer, "Status" smallint, "Purchase Date" date, "Ship Date" date, "Payment Due Date" date, "Total" numeric, "Paid" numeric, "Balance" numeric, "VendorResource" character varying) AS
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
	
	IF vendor_resource <> '' THEN		
		tem = ' v."VendorResource" = ''' || vendor_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."Status" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."PurchasedDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."PurchasedDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."PurchasedDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."PurchasedDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
		

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_pur_po_summary';
	ELSE
		sql = 'SELECT * FROM v_rpt_pur_po_summary v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pur_get_po_summary(integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pur_get_po_summary(integer, character varying, integer, character varying, character varying) IS '(POSReport)

Get Purchase Order Summary

rptPOSummary';


-- Function: sp_pur_get_product_cost(integer, character varying, integer, character varying, character varying, character varying)

-- DROP FUNCTION sp_pur_get_product_cost(integer, character varying, integer, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_pur_get_product_cost(IN store_code integer, IN vendor_resource character varying, IN category_id integer, IN product_resource character varying, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("Category" character varying, "Product Name" character varying, "PONumber" character varying, "Purchase Date" date, "Company" character varying, "StoreCode" integer, "Qty" numeric, "Price" numeric, "Total Cost" numeric, "VendorResource" character varying, "CategoryId" integer, "ProductResource" character varying) AS
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

	IF vendor_resource <> '' THEN		
		tem = ' v."VendorResource" = ''' || vendor_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
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
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."PurchasedDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."PurchasedDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."PurchasedDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."PurchasedDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_pur_product_cost';
	ELSE
		sql = 'SELECT * FROM v_rpt_pur_product_cost v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pur_get_product_cost(integer, character varying, integer, character varying, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pur_get_product_cost(integer, character varying, integer, character varying, character varying, character varying) IS '(POSReport)

Get Product Cost

rptProductCost';


-- Function: sp_pur_get_vendor_list(integer, character varying)

-- DROP FUNCTION sp_pur_get_vendor_list(integer, character varying);

CREATE OR REPLACE FUNCTION sp_pur_get_vendor_list(IN country_value integer, IN vendor_resource character varying)
  RETURNS TABLE("Company" character varying, "Phone" character varying, "Email" character varying, "Address" text, "StateProvinceId" integer, "PostalCode" character varying, "CountryId" integer, "VendorResource" character varying) AS
$BODY$
DECLARE sql text;
	tem text;
	condition boolean;
BEGIN
	condition = false;				
	IF country_value <> -1 THEN
		condition = true;
		sql = ' v."CountryId" = ' || country_value::text;
	END IF;

	IF vendor_resource <> '' THEN		
		tem = ' v."VendorResource" = ''' || vendor_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_pur_vendor_list';
	ELSE
		sql = 'SELECT * FROM v_rpt_pur_vendor_list v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pur_get_vendor_list(integer, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pur_get_vendor_list(integer, character varying) IS '(POSReport)

Get Vendor list

rptVendorList';


-- Function: sp_pur_get_vendor_product_list(integer, character varying, integer, character varying)

-- DROP FUNCTION sp_pur_get_vendor_product_list(integer, character varying, integer, character varying);

CREATE OR REPLACE FUNCTION sp_pur_get_vendor_product_list(IN store_code integer, IN vendor_resource character varying, IN category_id integer, IN product_resource character varying)
  RETURNS TABLE("Category" character varying, "Product Name" character varying, "StoreCode" integer, "Company" character varying, "AUCCost" numeric, "VendorResource" character varying, "CategoryId" integer, "ProductResource" character varying) AS
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

	IF vendor_resource <> '' THEN		
		tem = ' v."VendorResource" = ''' || vendor_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
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
		sql = 'SELECT * FROM v_rpt_pur_vendor_product_list';
	ELSE
		sql = 'SELECT * FROM v_rpt_pur_vendor_product_list v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pur_get_vendor_product_list(integer, character varying, integer, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pur_get_vendor_product_list(integer, character varying, integer, character varying) IS '(POSReport)

Get Vendor Product List

rptVendorProductList';

