-- Function: sp_pos_get_customer_profile(character varying)

-- DROP FUNCTION sp_pos_get_customer_profile(character varying);

CREATE OR REPLACE FUNCTION sp_pos_get_customer_profile(IN resource character varying)
  RETURNS TABLE("GuestNo" character varying, "Title" smallint, "Customer" text, "Phone" character varying, "Email" character varying, "Website" character varying, "Remark" character varying, "CellPhone" character varying, "IM" character varying, "BillAddress" character varying, "BillCity" character varying, "BillSate" integer, "BillPostalCode" character varying, "BillCountry" integer, "ShipAddress" character varying, "ShipCity" character varying, "ShipSate" integer, "ShipPostalCode" character varying, "ShipCountry" integer, "ETitle" smallint, "EmegencyContact" text, "EPhone" character varying, "ECellPhone" character varying, "ERelationship" character varying, "Image" bytea) AS
$BODY$
DECLARE sql text;
BEGIN
	
	RETURN QUERY (
		SELECT g."GuestNo",
		g."Title", COALESCE(g."LastName"::text,'') || ' '::text || COALESCE(g."FirstName"::text,'') || ' '::text as "Customer", 
		COALESCE(g."Phone1", g."Phone2") as "Phone", g."Email",  g."Website", g."Remark", g."CellPhone", g."IM",
		ga."AddressLine1" as "BillAddress", ga."City" AS "BillCity", ga."StateProvinceId" as "BillStateProvinceId", ga."PostalCode" as "BillPostalCode", ga."CountryId" as "BillCountryId",
		gs."AddressLine1" as "ShipAddress", gs."City" AS "ShipCity", gs."StateProvinceId", gs."PostalCode", gs."CountryId", gp."ETitle", 
		COALESCE(gp."ELastName"::text,'') || ' '::text || COALESCE(gp."EFirstName"::text,'') || ' '::text || COALESCE(gp."EMiddleName"::text, ''::text) AS "Emegency Contact",
		 gp."EPhone", gp."ECellPhone", gp."ERelationship", g."Picture"
		FROM "base_Guest" g 
			JOIN "base_GuestProfile" gp ON g."Id" = gp."GuestId"
			LEFT JOIN "base_GuestAddress" ga ON g."Id" = ga."GuestId" AND ga."AddressTypeId" = 2
			LEFT JOIN "base_GuestAddress" gs ON g."Id" = gs."GuestId" AND gs."AddressTypeId" = 3	
		WHERE g."Mark"	= 'C' --and g."Id" = 71
		AND g."Resource" = resource::uuid
	);
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_get_customer_profile(character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_get_customer_profile(character varying) IS '(POS) Get customer profile
';

-- Function: sp_pos_get_employee_information(character varying)

-- DROP FUNCTION sp_pos_get_employee_information(character varying);

CREATE OR REPLACE FUNCTION sp_pos_get_employee_information(IN resource character varying)
  RETURNS TABLE("Picture" bytea, "Title" smallint, "Employee" text, "Street" character varying, "City" character varying, "Sate" integer, "PostalCode" character varying, "Country" integer, "Phone1" character varying, "CellPhone" character varying, "Email" character varying, "SSN" character varying, "DOB" date, "Marital" smallint, "STitle" smallint, "SEmployee" text, "SRelationship" character varying, "SDOB" date, "SSSN" character varying, "SID" character varying, "SPhone" character varying, "SCellPhone" character varying, "SEmail" character varying, "SIM" character varying, "ETitle" smallint, "EmegencyContact" text, "EPhone" character varying, "ECellPhone" character varying, "ERelationship" character varying) AS
$BODY$
DECLARE sql text;
BEGIN
	
	RETURN QUERY (
		SELECT g."Picture", g."Title", g."LastName"::text || ' '::text || g."FirstName"::text || ' '::text || COALESCE(g."MiddleName", ''::bpchar)::text AS "Employee",  
		ga."AddressLine1" AS "PStreet", ga."City" AS "PCity", ga."StateProvinceId" AS "pState", 
		ga."PostalCode" AS "PPostalCode", ga."CountryId" AS "PCountry", g."Phone1", g."CellPhone", g."Email", gp."SSN", gp."DOB"::date AS "DOB", gp."Marital", gp."STitle", 
		COALESCE(gp."SLastName"::text,'') || ' '::text || COALESCE(gp."SFirstName"::text,'') || ' '::text || COALESCE(gp."SMiddleName", ''::bpchar)::text AS "Other", gp."SRelationShip",
		 gp."SDOB"::date AS "SDOB", gp."SSSN", gp."SIdentification", gp."SPhone", gp."SCellPhone", gp."SEmail", gp."SIM", gp."ETitle", 
		 COALESCE(gp."ELastName"::text,'') || ' '::text || COALESCE(gp."EFirstName"::text,'') || ' '::text || COALESCE(gp."EMiddleName", ''::bpchar)::text AS "Emegency", gp."EPhone", gp."ECellPhone", gp."ERelationship"
		   FROM "base_Guest" g
		   LEFT JOIN "base_GuestProfile" gp ON g."Id" = gp."GuestId"
		   LEFT JOIN "base_GuestAddress" ga ON ga."GuestId" = g."Id"
		  WHERE g."Mark" = 'E'::bpchar 
		AND g."Resource" = resource::uuid
	);
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_get_employee_information(character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_get_employee_information(character varying) IS '(POS) Get Employee inforamtion
';


-- Function: sp_pos_get_purchase_order(integer, boolean)

-- DROP FUNCTION sp_pos_get_purchase_order(integer, boolean);

CREATE OR REPLACE FUNCTION sp_pos_get_purchase_order(IN po_id integer, IN is_return boolean)
  RETURNS TABLE("PONo" character varying, "PODate" date, "PaymentTermDescription" character varying, "POCardImg" bytea, "ReturnFee" numeric, "Refund" numeric, "Balance" numeric) AS
$BODY$
BEGIN
	IF is_return THEN
		RETURN QUERY(	
			Select po."PurchaseOrderNo", po."PurchasedDate"::date, po."PaymentTermDescription", po."POCardImg", rt."ReturnFee", rt."TotalRefund", rt."Balance"
				From "base_PurchaseOrder" po 
				JOIN "base_ResourceReturn" rt ON po."Resource" = rt."DocumentResource"::uuid
				Where po."Id" = po_id AND rt."Mark" = 'PO'			
		);
	ELSE
		RETURN QUERY(	
			Select po."PurchaseOrderNo", po."PurchasedDate"::date, po."PaymentTermDescription", po."POCardImg",  po."Paid", po."Balance", 0.0
			    From "base_PurchaseOrder" po 
			    Where po."Id" = po_id
		);
	END IF;	

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_get_purchase_order(integer, boolean) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_get_purchase_order(integer, boolean) IS '(POS) get Purchase Order 
rptPurchaseOrder';


-- Function: sp_pos_get_purchase_order_details(integer, boolean)

-- DROP FUNCTION sp_pos_get_purchase_order_details(integer, boolean);

CREATE OR REPLACE FUNCTION sp_pos_get_purchase_order_details(IN po_id integer, IN is_return boolean)
  RETURNS TABLE("ItemCode" character varying, "ItemName" character varying, "Attribute" character varying, "Size" character varying, "Qty" numeric, "BaseUOM" character varying, "Price" numeric, "Amount" numeric, "IsReturn" boolean) AS
$BODY$
BEGIN
	IF is_return THEN
		RETURN QUERY(	
			Select rd."ItemCode", rd."ItemName", rd."ItemAtribute", rd."ItemSize", rd."ReturnQty", pd."BaseUOM", rd."Price", rd."Amount", rd."IsReturned"
			From "base_PurchaseOrder" po INNER JOIN  "base_PurchaseOrderDetail" pd ON po."Id" = pd."PurchaseOrderId"						
				INNER JOIN "base_ResourceReturnDetail" rd ON  pd."Resource" = rd."OrderDetailResource"::uuid
				INNER JOIN "base_ResourceReturn" rt on rt."Id" = rd."ResourceReturnId" AND rt."Mark" = 'PO'
			Where pd."PurchaseOrderId" = po_id
		);
	ELSE
		RETURN QUERY(	
			Select pd."ItemCode", pd."ItemName", pd."ItemAtribute", pd."ItemSize", pd."Quantity", pd."BaseUOM", pd."Price", pd."Amount" , false
			    From "base_PurchaseOrderDetail" pd
			    Where pd."PurchaseOrderId" = po_id
		);
	END IF;
		

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_get_purchase_order_details(integer, boolean) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_get_purchase_order_details(integer, boolean) IS '(POS) Get purchase order details by PO resource';



-- Function: sp_pos_get_vendor_profile(character varying)

-- DROP FUNCTION sp_pos_get_vendor_profile(character varying);

CREATE OR REPLACE FUNCTION sp_pos_get_vendor_profile(IN resource character varying)
  RETURNS TABLE("Picture" bytea, "GuestNo" character varying, "Company" character varying, "Street" character varying, "City" character varying, "State" integer, "PostalCode" character varying, "Country" integer, "Website" character varying, "Phone1" character varying, "Phone2" character varying, "CellPhone" character varying, "Fax" character varying, "Email" character varying, "IM" character varying, "FedTaxId" character varying, "PaymentTermDescription" character varying, "CreaditLine" numeric, "Remark" character varying) AS
$BODY$
DECLARE sql text;
BEGIN
	
	RETURN QUERY (		
		SELECT g."Picture", g."GuestNo", g."Company",
			ga."AddressLine1", ga."City", ga."StateProvinceId", ga."PostalCode", ga."CountryId",
			g."Website", g."Phone1", g."Phone2", g."CellPhone", g."Fax", g."Email", g."IM", gad."FedTaxId", g."PaymentTermDescription", g."CreditLine", g."Remark"
		FROM "base_Guest" g 
			LEFT JOIN "base_GuestAddress" ga on ga."GuestId" = g."Id"
			LEFT JOIN "base_GuestAdditional" gad on gad."GuestId" = g."Id"
		WHERE g."Mark" = 'V'::bpchar --AND "GuestNo" like '%3734'
		AND g."Resource" = resource::uuid
	);
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_get_vendor_profile(character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_get_vendor_profile(character varying) IS '(POS) Get Vendor Profile';


-- Function: sp_pos_so_get_pick_pack(character varying)

-- DROP FUNCTION sp_pos_so_get_pick_pack(character varying);

CREATE OR REPLACE FUNCTION sp_pos_so_get_pick_pack(IN resource character varying)
  RETURNS TABLE("ItemCode" character varying, "ItemName" character varying, "Attribute" character varying, "Size" character varying, "PackedQty" numeric, "IsShipped" boolean, "ShipDate" date, "Id" bigint) AS
$BODY$
BEGIN
	RETURN QUERY (
		SELECT ssd."ItemCode", ssd."ItemName", ssd."ItemAtribute", ssd."ItemSize", ssd."PackedQty", ss."IsShipped", ss."ShipDate"::date, ss."Id"
		FROM "base_SaleOrder" so
			JOIN "base_SaleOrderShip" ss on ss."SaleOrderResource"::uuid = so."Resource"
			JOIN "base_SaleOrderShipDetail" ssd on ss."Resource" = ssd."SaleOrderShipResource"::uuid
		WHERE so."Resource"::character varying = resource
		ORDER BY ssd."Id"
	);
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_so_get_pick_pack(character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_so_get_pick_pack(character varying) IS '(POS) get Pick and Pack by Sale Order resource';



-- Function: sp_pos_so_get_sale_order(character varying, boolean)

-- DROP FUNCTION sp_pos_so_get_sale_order(character varying, boolean);

CREATE OR REPLACE FUNCTION sp_pos_so_get_sale_order(IN so_resource character varying, IN is_returned boolean)
  RETURNS TABLE("StoreCode" integer, "SOCardImg" bytea, "SONumber" character varying, "OrderDate" date, "Cashier" character varying, "SubTotal" numeric, "TaxCode" character varying, "Discount" numeric, "Shipping" numeric, "Total" numeric, "Tip" numeric, "TaxAmount" numeric, "Remark" character varying, "IsRedeeem" boolean, "RewardAmount" numeric) AS
$BODY$
BEGIN
	IF (is_returned = TRUE) THEN
		RETURN QUERY (
			  SELECT so."StoreCode", so."SOCardImg", so."SONumber", so."OrderDate"::date, so."UserCreated"
				, so."Paid", so."TaxCode", rt."Redeemed", rt."TotalRefund", rt."Balance", 0.0, 0.0, so."Remark", false, 0.0
			 FROM "base_SaleOrder" so
				JOIN "base_ResourceReturn" rt on rt."DocumentResource"::uuid = so."Resource" AND rt."Mark" = 'SO'
			WHERE CAST(so."Resource" AS CHARACTER VARYING) = so_resource	 
		);
	ELSE	
		RETURN QUERY (
			  SELECT so."StoreCode", so."SOCardImg", so."SONumber", so."OrderDate"::date, so."UserCreated", so."SubTotal", so."TaxCode", 
				so."DiscountAmount" AS "Discount", so."Shipping", so."Total", sum(rpd."Tip") AS "Tip", 
				so."TaxAmount", so."Remark", so."IsRedeeem", so."RewardAmount"
			 FROM "base_SaleOrder" so
				 JOIN "base_ResourcePayment" rp ON rp."DocumentResource"::uuid = so."Resource"
				 JOIN "base_ResourcePaymentDetail" rpd ON rpd."ResourcePaymentId" = rp."Id"
			WHERE CAST(so."Resource" AS CHARACTER VARYING) = so_resource
			 GROUP BY so."StoreCode", so."SOCardImg", so."SONumber", so."OrderDate"::date, so."UserCreated", so."SubTotal", so."TaxCode", 
				so."DiscountAmount", so."Shipping", so."Total", so."TaxAmount", so."Remark", so."IsRedeeem", so."RewardAmount"	 
		);
	END IF;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_so_get_sale_order(character varying, boolean) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_so_get_sale_order(character varying, boolean) IS '(POS) Get Sale Order
rptSODetails';


-- Function: sp_pos_so_get_sale_order_details(integer, boolean)

-- DROP FUNCTION sp_pos_so_get_sale_order_details(integer, boolean);

CREATE OR REPLACE FUNCTION sp_pos_so_get_sale_order_details(IN sale_order_id integer, IN is_return boolean)
  RETURNS TABLE("ProductName" character varying, "RegularPrice" numeric, "DiscountAmount" numeric, "Qty" numeric, "UOM" character varying, "SubTotal" numeric, "IsReturned" boolean) AS
$BODY$
BEGIN
	IF is_return THEN
		RETURN QUERY (		
			SELECT sd."ItemName", rd."Price" , rd."Discount", rd."ReturnQty", sd."UOM",  rd."Amount" + rd."VAT", rd."IsReturned"
			FROM "base_SaleOrderDetail" sd 
				JOIN "base_ResourceReturnDetail" rd ON rd."OrderDetailResource"::uuid = sd."Resource"
				JOIN "base_ResourceReturn" rt on rt."Id" = rd."ResourceReturnId" AND rt."Mark" = 'SO'
			WHERE sd."SaleOrderId" = sale_order_id 
			Order by sd."Id"

		);
	ELSE	
		RETURN QUERY (		
			SELECT sd."ItemName", 
				CASE (sd."ItemCode" <> '111111111111111' AND sd."ItemCode" <> '222222222222222')
					WHEN TRUE THEN sd."RegularPrice"
					ELSE sd."SalePrice"
				END AS "RegularPrice"
				, sd."TotalDiscount", sd."Quantity", sd."UOM",  sd."SubTotal", false
			FROM "base_SaleOrderDetail" sd 
			WHERE sd."SaleOrderId" = sale_order_id
			Order by sd."Id"
			   
		);
	END IF;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_so_get_sale_order_details(integer, boolean) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_so_get_sale_order_details(integer, boolean) IS '(POS) Get sale order details
rptSODetails';



-- Function: sp_pos_so_resource_payment_by_resource(character varying)

-- DROP FUNCTION sp_pos_so_resource_payment_by_resource(character varying);

CREATE OR REPLACE FUNCTION sp_pos_so_resource_payment_by_resource(IN so_resource character varying)
  RETURNS TABLE("PaymentMethod" character varying, "CardType" smallint, "Paid" numeric, "Reference" character varying) AS
$BODY$
BEGIN
	RETURN QUERY (
			SELECT  pd."PaymentMethod", pd."CardType", pd."Paid", pd."Reference"
			FROM "base_ResourcePayment" rp INNER JOIN "base_ResourcePaymentDetail" pd on rp."Id" = pd."ResourcePaymentId"
			WHERE rp."DocumentResource"::character varying = so_resource
		EXCEPT 
			SELECT  pd."PaymentMethod", pd."CardType", pd."Paid", pd."Reference"
			FROM "base_ResourcePayment" rp INNER JOIN "base_ResourcePaymentDetail" pd on rp."Id" = pd."ResourcePaymentId"
			WHERE pd."PaymentMethodId" = 4 AND pd."CardType" = 0 
			AND rp."DocumentResource"::character varying = so_resource
			ORDER BY 1	 
	);
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_pos_so_resource_payment_by_resource(character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_pos_so_resource_payment_by_resource(character varying) IS '(POS) - Get all resource payment
rptSODetails';

