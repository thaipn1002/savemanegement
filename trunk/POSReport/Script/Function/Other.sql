-- Function: checkserialnumber(character varying, character varying)

-- DROP FUNCTION checkserialnumber(character varying, character varying);

CREATE OR REPLACE FUNCTION checkserialnumber("partNumber" character varying, "serialNumber" character varying)
  RETURNS boolean AS
$BODY$BEGIN
RETURN (SELECT COUNT(*)
FROM 
  stockadjustmentdetailserial sads
WHERE 
  sads.serialnumber = $2 AND 
  sads.partnumber = $1) > 0;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION checkserialnumber(character varying, character varying) OWNER TO postgres;


-- Function: clearalldata(character varying)

-- DROP FUNCTION clearalldata(character varying);

CREATE OR REPLACE FUNCTION clearalldata(username character varying)
  RETURNS void AS
$BODY$
DECLARE
    statements CURSOR FOR
        SELECT tablename FROM pg_tables
        WHERE tablename not like 'rpt_%' and tablename <> 'base_CustomField' and tablename <> 'base_UserRight' and tableowner = username AND schemaname = 'public';
BEGIN
    FOR stmt IN statements LOOP
          IF stmt.tablename = 'tims_TimeLogPermission' THEN
	     EXECUTE 'TRUNCATE TABLE ' || quote_ident(stmt.tablename) || ' CASCADE; ALTER SEQUENCE ' || quote_ident(stmt.tablename || '_TimeLogId_seq') || ' RESTART WITH 1;';
          ELSE
             EXECUTE 'TRUNCATE TABLE ' || quote_ident(stmt.tablename) || ' CASCADE; ALTER SEQUENCE ' || quote_ident(stmt.tablename || '_Id_seq') || ' RESTART WITH 1;';
          END IF; 
    END LOOP;
    
UPDATE "base_CustomField" set "Label" = "FieldName";

INSERT INTO "base_Configuration"(
            "CompanyName", "Address", "City", "State", "ZipCode", "CountryId", 
            "Phone", "Fax", "Email", "Website", "EmailPop3Server", "EmailPop3Port", 
            "EmailAccount", "EmailPassword", "IsBarcodeScannerAttached", 
            "IsEnableTouchScreenLayout", "IsAllowTimeClockAttached", "IsAllowCollectTipCreditCard", 
            "IsAllowMutilUOM", "DefaultMaximumSticky", "DefaultPriceSchema", 
            "DefaultPaymentMethod", "DefaultSaleTaxLocation", "DefaultTaxCodeNewDepartment", 
            "DefautlImagePath", "DefautlDiscountScheduleTime", "DateCreated", 
            "UserCreated", "TotalStore", "IsRequirePromotionCode", "DefaultDiscountType", 
            "DefaultDiscountStatus", "LoginAllow", "Logo", "DefaultScanMethod", 
            "TipPercent", "AcceptedPaymentMethod", "AcceptedCardType", "IsRequireDiscountReason", 
            "WorkHour", "DefaultShipUnit", "DefaultCashiedUserName", 
            "KeepLog", "IsAllowShift", "DefaultLanguage", "TimeOutMinute", 
            "IsAutoLogout", "IsBackupWhenExit", "BackupEvery", "BackupPath", 
            "IsAllowRGO", "IsAllowChangeOrder", "IsAllowNegativeStore", "AcceptedGiftCardMethod", 
            "IsRewardOnTax", "IsRewardOnMultiPayment", "IsIncludeReturnFee", 
            "ReturnFeePercent", "IsRewardLessThanDiscount", "CurrencySymbol", 
            "DecimalPlaces", "FomartCurrency", "PasswordFormat", "KeepBackUp", 
            "CostMethod", 
            "StoreCode", "IsLive", "POSId", "IsRewardOnDiscount", 
            "IsCalRewardAfterRedeem", "IsRewardOnRetailer", "ReceiptMessage",
            "NegativeNumber","TextNumberAlign","IsAUPPG","IsStateCode","IsManualGenerate","IsAllowFirstCap","DataSource",
            "IsSendEmailCustomer","IsAllowAntiExemptionTax","IsManualPriceCalculation","IsAllowPayMultiReward","IsSumCashReward",
            "IsAllwayCommision","ReminderDay","WeekHour","RefundVoucherThresHold")
    VALUES ('Smart POS Company', 'Default Address', 'Default City', 0, 0, 0, 
            '', '', '', '', '', 0, 
            '', '', true, 
            false, false, false, 
            true, 5, 0, 
            0, 0, '', 
            '', 12, now(), 
            '', 1, false, 0, 
            0, 3, null, 0, 
            0, 0, 0, true, 
            8, 0, true, 
            7, false, 'EN', 10, 
            true, true, 1, '', 
            false, false, true, 0, 
            false, false, false, 
            0, false, '$', 
            2, 'en-US', '((?=.*[^a-zA-Z])(?=.*[a-z])(?=.*[A-Z])(?!\s).{8,})',7,
            0,  
            0, false, 0, false, 
            false,false, 'Thank you',
            0,1,true,true,false,true,'train_pos2013',
            false,false,false,true,true,
            false,0,40,0);
           
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION clearalldata(character varying) OWNER TO postgres;


-- Function: newid()

-- DROP FUNCTION newid();

CREATE OR REPLACE FUNCTION newid()
  RETURNS uuid AS
$BODY$
 SELECT CAST(md5(current_database()|| user ||current_timestamp ||random()) as uuid)
$BODY$
  LANGUAGE sql VOLATILE
  COST 100;
ALTER FUNCTION newid() OWNER TO postgres;


-- Function: sp_check_report_code(character)

-- DROP FUNCTION sp_check_report_code(character);

CREATE OR REPLACE FUNCTION sp_check_report_code(code character)
  RETURNS boolean AS
$BODY$
DECLARE resuilt BOOLEAN;
BEGIN
	SELECT count(*) > 0 INTO resuilt FROM "rpt_Report" r WHERE lower(r."Code") = lower($1);
	RETURN resuilt;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION sp_check_report_code(character) OWNER TO postgres;
COMMENT ON FUNCTION sp_check_report_code(character) IS '(POSReport) Check duplicate Report code';


-- Function: sp_check_user_login(character varying, character varying)

-- DROP FUNCTION sp_check_user_login(character varying, character varying);

CREATE OR REPLACE FUNCTION sp_check_user_login(IN usr character varying, IN pwd character varying)
  RETURNS TABLE("IsActive" boolean, "NotExpiry" boolean, "Resource" character varying) AS
$BODY$
BEGIN
return query(
		SELECT u."IsActive", CASE WHEN COALESCE(u."ExpiryDate"::date - now()::date, 0) >= 0 THEN true
	ELSE false END , u."Resource"::character varying
		FROM "rpt_User" u
		WHERE lower(u."LoginName") = lower(usr) and  pwd = u."Password");

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_check_user_login(character varying, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_check_user_login(character varying, character varying) IS '(POSReport) Check user login';


-- Function: sp_get_payment_method(integer, date, character varying)

-- DROP FUNCTION sp_get_payment_method(integer, date, character varying);

CREATE OR REPLACE FUNCTION sp_get_payment_method(IN paymentmethod_id integer, IN payment_date date, IN shift character varying)
  RETURNS TABLE("CardName" character varying, "Count" bigint, "Paid" numeric, "CardType" smallint) AS
$BODY$
DECLARE sql text;
tem text;
BEGIN
	sql = 'SELECT rd."PaymentMethod", COUNT(rd."PaymentMethodId"), SUM(rd."Paid"), rd."CardType" 
		   FROM "base_ResourcePayment" r
		   JOIN "base_ResourcePaymentDetail" rd ON rd."ResourcePaymentId" = r."Id"
		   WHERE r."DateCreated"::Date  =''' || payment_date || ''' AND rd."PaymentMethodId" =' || paymentmethod_id ||'::integer';
	IF shift <> '' THEN 
		sql = sql || ' AND r."Shift" =''' || shift::character varying || '''';
	END IF;
	sql = sql || ' GROUP BY rd."PaymentMethod", rd."CardType"
		   HAVING rd."CardType" <> 0';
	RETURN QUERY EXECUTE sql;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_get_payment_method(integer, date, character varying) OWNER TO postgres;
COMMENT ON FUNCTION sp_get_payment_method(integer, date, character varying) IS '(POS) Get payment method';


-- Function: sp_get_reminder()

-- DROP FUNCTION sp_get_reminder();

CREATE OR REPLACE FUNCTION sp_get_reminder()
  RETURNS boolean AS
$BODY$
BEGIN
	DELETE FROM "base_CustomerReminder" cr
		WHERE cr."ReminderTypeId" = 0 
			AND EXTRACT(MONTH FROM now()) > EXTRACT(MONTH FROM cr."DOB")
			OR (EXTRACT(MONTH FROM now()) = EXTRACT(MONTH FROM cr."DOB") AND EXTRACT(DAY FROM now()) > EXTRACT(DAY FROM cr."DOB"));
	INSERT INTO "base_CustomerReminder"(
		    "GuestResource", "ReminderTypeId", "DOB", "Name", "Company", "Phone", "Email")
	    ( SELECT g."Resource", 0 AS "ReminderTypeId", gp."DOB"::date, 
		CASE g."Title" 
			WHEN 0 THEN ''
			WHEN 1 THEN 'Mr.'
			WHEN 2 THEN 'Ms. '
			WHEN 3 THEN 'Mrs. '
			WHEN 4 THEN 'Prof. '			
		END
	    ||(((g."LastName"::text || ', '::text) || g."FirstName"::text) || ' '::text) || COALESCE(g."MiddleName"::text, ''::text) AS "Name", g."Company", COALESCE(g."Phone1", g."Phone2") AS "Phone", g."Email"
	FROM "base_GuestProfile" gp 
		INNER JOIN "base_Guest" g on g."Id" = gp."GuestId" and g."Mark"='C' and g."IsPurged" = false
	WHERE  (EXTRACT(MONTH FROM gp."DOB") || '') || (EXTRACT(DAY FROM gp."DOB") || '')  
	       in 
	       (SELECT (EXTRACT(MONTH FROM CURRENT_DATE + s.a) || '') || (EXTRACT(DAY FROM CURRENT_DATE + s.a) || '') 
	        FROM GENERATE_SERIES(0, (Select "ReminderDay" From "base_Configuration")) AS s(a))
	
		AND gp."GuestId" > 0
		AND g."Resource" not in (SELECT cr."GuestResource"::uuid FROM "base_CustomerReminder" cr WHERE cr."ReminderTypeId" = 0));
		RETURN TRUE;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION sp_get_reminder() OWNER TO postgres;
COMMENT ON FUNCTION sp_get_reminder() IS '(POS) 
';


-- Function: sp_get_user_permission(character varying, integer)

-- DROP FUNCTION sp_get_user_permission(character varying, integer);

CREATE OR REPLACE FUNCTION sp_get_user_permission(IN resource character varying, IN permission_type integer)
  RETURNS TABLE("Type" smallint, "Code" character varying, "IsView" boolean, "IsPrint" boolean, "Right" boolean) AS
$BODY$
DECLARE sql text;
BEGIN
	sql = 'SELECT up."Type", up."Code", up."IsView", up."IsPrint", up."Right"
	FROM "rpt_Permission" up 
	WHERE up."UserResource" =''' || resource || '''';
	IF (permission_type = -1) THEN
		sql = sql || ' ORDER BY up."Type"';
	ELSEIF (permission_type = -2) THEN
		sql = sql || ' AND up."Type" <> 0 ORDER BY up."Type"';
	ELSE 
		sql = sql || ' AND up."Type" = ' || permission_type || ' ORDER BY up."Type"';
	END IF;	
	RETURN query EXECUTE sql;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION sp_get_user_permission(character varying, integer) OWNER TO postgres;
COMMENT ON FUNCTION sp_get_user_permission(character varying, integer) IS '(POSReport) Check user permission
permission_type: 
-2 Report Permission and Menu Permission
-1. All Permission
0. Group Permission
1. Report Permission
2. Menu Permission
';
