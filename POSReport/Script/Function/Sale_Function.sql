-- Function: sp_sale_commission_details(integer, character varying, character varying, character varying, character varying)

-- DROP FUNCTION sp_sale_commission_details(integer, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_commission_details(IN store_code integer, IN sale_rep character varying, IN product_resource character varying, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "Sale Rep" character varying, "ItemName" character varying, "StoreCode" integer, "Order Date" date, "Close Sale" numeric, "Commission Amount" numeric, "Remark" character varying, "Total Cost" numeric, "SaleRepResource" character varying, "ProductResource" character varying) AS
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

	IF sale_rep <> '' THEN		
		tem = ' v."SaleRepResource" = ''' || sale_rep || '''';
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
		sql = 'SELECT * FROM v_rpt_sale_commission_details';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_commission_details v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_commission_details(integer, character varying, character varying, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_commission_details(integer, character varying, character varying, character varying, character varying) IS '(POSReport)

Get Sale Commission Details

rptSaLeCommissionDetails';


-- Function: sp_sale_customer_order_history(integer, character varying, integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_sale_customer_order_history(integer, character varying, integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_customer_order_history(IN store_code integer, IN customer_resource character varying, IN category_id integer, IN product_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "Customer" text, "ProductName" character varying, "Category" character varying, "StoreCode" integer, "OrderStatus" smallint, "OrderDate" date, "Quantity" numeric, "Amount" numeric, "CustomerResource" character varying, "ProductResource" character varying, "CategoryId" integer) AS
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
	
	IF customer_resource <> '' THEN
		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
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
	
	IF status <> -1 THEN
		tem = ' v."OrderStatus" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
	
	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_customer_order_history'::text;
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_customer_order_history v  WHERE '::text || sql;
	END IF;

RETURN QUERY EXECUTE sql;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_customer_order_history(integer, character varying, integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_customer_order_history(integer, character varying, integer, character varying, integer, character varying, character varying) IS '(POSReport)

Get Customer order history

rptCustomerOrderHistory';


-- Function: sp_sale_get_customer_payment_details(integer, character varying, integer, character varying, character varying, character varying, character varying)

-- DROP FUNCTION sp_sale_get_customer_payment_details(integer, character varying, integer, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_customer_payment_details(IN store_code integer, IN customer_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying, IN ship_from character varying, IN ship_to character varying)
  RETURNS TABLE("Customer" text, "SONumber" character varying, "OrderStatus" smallint, "StoreCode" integer, "InvoiceDate" date, "DateLeft" integer, "DatePaid" date, "Sale Total" numeric, "Amount Paid" numeric, "Balance" numeric, "CustomerResource" character varying) AS
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
	
	IF customer_resource <> '' THEN		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF status <> -1 THEN
		tem = ' v."OrderStatus" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;	

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."InvoiceDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."InvoiceDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."InvoiceDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."InvoiceDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF ship_from <> '' AND ship_to <> '' THEN
		IF ship_from::date < ship_to::date THEN
			tem = 'v."Date Paid"::Date BETWEEN ''' || ship_from || ''' AND ''' ||ship_to || '''';
		ELSE		
			tem = 'v."Date Paid"::Date BETWEEN ''' || ship_to || ''' AND ''' ||ship_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF ship_from <> '' THEN
			tem = ' v."Date Paid"::Date >= ''' || ship_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF ship_to <> '' THEN
			tem =  'v."Date Paid"::Date <= ''' || ship_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;		

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_customer_payment_details';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_customer_payment_details v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_customer_payment_details(integer, character varying, integer, character varying, character varying, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_customer_payment_details(integer, character varying, integer, character varying, character varying, character varying, character varying) IS '(POSReport)

Get customer payment details

rptCustomerPaymentDetails';


-- Function: sp_sale_get_customer_payment_summary(integer, character varying, character varying, character varying, character varying, character varying)

-- DROP FUNCTION sp_sale_get_customer_payment_summary(integer, character varying, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_customer_payment_summary(IN store_code integer, IN customer_resource character varying, IN order_from character varying, IN order_to character varying, IN ship_from character varying, IN ship_to character varying)
  RETURNS TABLE("Customer" text, "StoreCode" integer, "TotalAmount" numeric, "TotalPaid" numeric, "Balance" numeric, "LastOrder" date, "LastPayment" date, "CustomerResource" character varying) AS
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
	
	IF customer_resource <> '' THEN		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."LastOrder"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."LastOrder"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."LastOrder"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."LastOrder"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF ship_from <> '' AND ship_to <> '' THEN
		IF ship_from::date < ship_to::date THEN
			tem = 'v."LastPayment"::Date BETWEEN ''' || ship_from || ''' AND ''' ||ship_to || '''';
		ELSE		
			tem = 'v."LastPayment"::Date BETWEEN ''' || ship_to || ''' AND ''' ||ship_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF ship_from <> '' THEN
			tem = ' v."LastPayment"::Date >= ''' || ship_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF ship_to <> '' THEN
			tem =  'v."LastPayment"::Date <= ''' || ship_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;		
		

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_customer_payment_summary';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_customer_payment_summary v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_customer_payment_summary(integer, character varying, character varying, character varying, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_customer_payment_summary(integer, character varying, character varying, character varying, character varying, character varying) IS '(POSReport)

Get customer payment summary

rptCustomerPaymentSummary';


-- Function: sp_sale_get_product_customer(integer, character varying, integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_sale_get_product_customer(integer, character varying, integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_product_customer(IN store_code integer, IN customer_resource character varying, IN category_id integer, IN product_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("Category" character varying, "ProductName" character varying, "StoreCode" integer, "SONumber" character varying, "Customer" text, "OrderDate" date, "Status" smallint, "SoldQty" numeric, "CloseAmount" numeric, "CategoryId" integer, "CustomerResource" character varying, "ProductResource" character varying) AS
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
	
	IF customer_resource <> '' THEN		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
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
	
	IF status <> -1 THEN
		tem = ' v."OrderStatus" = ' || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;


	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_product_customer';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_product_customer v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_product_customer(integer, character varying, integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_product_customer(integer, character varying, integer, character varying, integer, character varying, character varying) IS '(POSReport)

Get product customer

rptProductCustomer
';


-- Function: sp_sale_get_sale_by_product_details(integer, integer, character varying, character varying, character varying)

-- DROP FUNCTION sp_sale_get_sale_by_product_details(integer, integer, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_sale_by_product_details(IN store_code integer, IN category_id integer, IN product_resource character varying, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("Category" character varying, "Code" character varying, "ProductName" character varying, "StoreCode" integer, "Attribute" character varying, "Size" character varying, "OrderDate" date, "OrderNumber" character varying, "SoldQuantity" numeric, "UOM" character varying, "SaleAmount" numeric, "Product Resource" character varying, "Category Id" integer) AS
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
		tem = ' v."ProductResource" = '''::text || product_resource::text || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
		
	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_by_product_details';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_by_product_details v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_sale_by_product_details(integer, integer, character varying, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_sale_by_product_details(integer, integer, character varying, character varying, character varying) IS '(POSReport)

Get sale by product details

rptSaleByProductDetails';


-- Function: sp_sale_get_sale_by_product_summary(integer, integer, character varying)

-- DROP FUNCTION sp_sale_get_sale_by_product_summary(integer, integer, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_sale_by_product_summary(IN store_code integer, IN category_id integer, IN product_resource character varying)
  RETURNS TABLE("Category" character varying, "Code" character varying, "ProductName" character varying, "StoreName" integer, "Attribute" character varying, "Size" character varying, "SoldQuantity" numeric, "TotolSale" numeric, "TotalCOGS" numeric, "SaleProfit" numeric, "PurchasedQuantity" numeric, "PurchasedSubTotal" numeric, "TotalProfit" numeric, "CategoryId" integer, "ProductResource" character varying) AS
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
		tem = ' v."ProductResource" = '''::text || product_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_by_product_summary';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_by_product_summary v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_sale_by_product_summary(integer, integer, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_sale_by_product_summary(integer, integer, character varying) IS '(POSReport)

rptSaleByProductSummary';


-- Function: sp_sale_get_sale_order_operational(integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_sale_get_sale_order_operational(integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_sale_order_operational(IN store_code integer, IN customer_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "Status" smallint, "Customer" text, "StoreCode" integer, "Order Date" date, "SaleAmount" numeric, "CustomerResource" character varying) AS
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
	
	IF customer_resource <> '' THEN		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."OrderStatus" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
		
	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_order_operational';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_order_operational v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_sale_order_operational(integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_sale_order_operational(integer, character varying, integer, character varying, character varying) IS '(POSReport)

Get Sale Order Operational

rptSaleOrderOperational';


-- Function: sp_sale_get_sale_order_summary(integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_sale_get_sale_order_summary(integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_sale_order_summary(IN store_code integer, IN customer_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "Customer" text, "StoreCode" integer, "OrderDate" date, "OrderStatus" smallint, "SubTotal" numeric, "TaxAmount" numeric, "DiscountAmount" numeric, "Shipping" numeric, "Total" numeric, "Deposit" numeric, "CustomerResource" character varying) AS
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
	
	IF customer_resource <> '' THEN
		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."OrderStatus" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
		

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_order_summary';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_order_summary v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;


END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_sale_order_summary(integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_sale_order_summary(integer, character varying, integer, character varying, character varying) IS '(POSReport)

Get Sale Order Summary

rptSaleOrderSummary';


-- Function: sp_sale_get_sale_profit_summary(integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_sale_get_sale_profit_summary(integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_sale_profit_summary(IN store_code integer, IN customer_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "Status" smallint, "Customer" text, "StoreCode" integer, "DateCreate" date, "SaleAmount" numeric, "COGS" numeric, "Gross Profit" numeric, "CustomerResource" character varying) AS
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
	
	IF customer_resource <> '' THEN		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF status <> -1 THEN
		tem = ' v."OrderStatus" = '::text || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
		

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_profit_summary';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_profit_summary v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_sale_profit_summary(integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_sale_profit_summary(integer, character varying, integer, character varying, character varying) IS '(POSReport) 

Get sale Profit Summary

rptSaleProfitSummary';


-- Function: sp_sale_get_voided_invoice(integer, character varying, character varying)

-- DROP FUNCTION sp_sale_get_voided_invoice(integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_get_voided_invoice(IN store_code integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "SalePrice" numeric, "DateCreated" timestamp without time zone, "VoidedReason" character varying, "UserCreated" character varying, "StoreCode" integer) AS
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
	
	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."DateUpdated"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."DateUpdated"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."DateUpdated"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."DateUpdated"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;

	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_voided_invoice';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_voided_invoice v  WHERE ' || sql;
	END IF;

	RETURN QUERY EXECUTE sql;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_get_voided_invoice(integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_get_voided_invoice(integer, character varying, character varying) IS '(POSReport)

Get Voided invoice

rptVoidedInvoice';


-- Function: sp_sale_representative(integer, character varying, integer, character varying, character varying)

-- DROP FUNCTION sp_sale_representative(integer, character varying, integer, character varying, character varying);

CREATE OR REPLACE FUNCTION sp_sale_representative(IN store_code integer, IN customer_resource character varying, IN status integer, IN order_from character varying, IN order_to character varying)
  RETURNS TABLE("SONumber" character varying, "Customer" text, "StoreCode" integer, "OrderStatus" smallint, "SaleRep" character varying, "Close Sale" numeric, "OrderDate" date, "CustomerResource" character varying) AS
$BODY$ 
DECLARE sql text;
	tem text;
	condition boolean;
	orderto text;	
BEGIN	
	condition = false;	
	
	IF store_code <> -1 THEN
		condition = true;
		sql = ' v."StoreCode" = ' || store_code::text;
	END IF;
	
	IF customer_resource <> '' THEN
		
		tem = ' v."CustomerResource" = ''' || customer_resource || '''';
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;
			
	IF status <> -1 THEN
		tem = ' v."OrderStatus" = ' || status::text;
		IF condition = true THEN
			sql = sql || ' AND ' || tem;
		ELSE
			condition = true;
			sql = tem;
		END IF;
	END IF;

	IF order_from <> '' AND order_to <> '' THEN
		IF order_from::date < order_to::date THEN
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_from || ''' AND ''' ||order_to || '''';
		ELSE		
			tem = 'v."OrderDate"::Date BETWEEN ''' || order_to || ''' AND ''' ||order_from || '''';
		END IF;
		IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
	ELSE	
		IF order_from <> '' THEN
			tem = ' v."OrderDate"::Date >= ''' || order_from || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
		
		IF order_to <> '' THEN
			tem =  'v."OrderDate"::Date <= ''' || order_to || '''';
			IF condition = true THEN
				sql = sql || ' AND ' || tem;
			ELSE
				condition = true;
				sql = tem;
			END IF;
		END IF;
	END IF;
	
	IF condition = FALSE THEN
		sql = 'SELECT * FROM v_rpt_sale_representative';
	ELSE
		sql = 'SELECT * FROM v_rpt_sale_representative v  WHERE ' || sql;
	END IF;

RETURN QUERY EXECUTE sql;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_sale_representative(integer, character varying, integer, character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_sale_representative(integer, character varying, integer, character varying, character varying) IS '(POSReport)

Get sale Representative

rptSaleRepresentative';
