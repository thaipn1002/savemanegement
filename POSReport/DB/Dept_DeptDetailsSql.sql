DROP VIEW v_rpt_sale_dept_summary;

CREATE OR REPLACE VIEW v_rpt_sale_dept_summary AS 
 SELECT DISTINCT (((g."FirstName"::text || ', '::text) || g."LastName"::text) || ' '::text) || COALESCE(g."MiddleName"::text, ''::text) AS "Customer", s."StoreCode", s."Total", s."Paid", s."Balance", COALESCE(s."DateUpdated", s."OrderDate")::date AS "LastOrder", rp."DateCreated"::date AS "LastPayment", s."SONumber", s."CustomerResource"
   FROM "base_SaleOrder" s
   JOIN "base_ResourcePayment" rp ON s."Resource" = rp."DocumentResource"::uuid
   JOIN "base_Guest" g ON g."Resource" = s."CustomerResource"::uuid AND s."Mark" = 'SO'::bpchar
  WHERE s."OrderStatus" <> 6 AND s."Balance" > 0::numeric;

ALTER TABLE v_rpt_sale_dept_summary OWNER TO postgres;
COMMENT ON VIEW v_rpt_sale_dept_summary IS '(POSReport) Get all sale dept summary

Except Paid in full
WHERE s."OrderStatus" != 6;';



DROP VIEW v_rpt_sale_dept_details;

CREATE OR REPLACE VIEW v_rpt_sale_dept_details AS 
 SELECT (((g."FirstName"::text || ', '::text) || g."LastName"::text) || ' '::text) || COALESCE(g."MiddleName"::text, ''::text) AS "Customer", s."SONumber", s."OrderStatus", s."StoreCode", rp."DateCreated"::date AS "InvoiceDate", rp."DateCreated"::date - s."DateCreated"::date AS "Date Left", rp."DateCreated"::date AS "Date Paid", rp."TotalAmount" AS "Total Amount", rp."TotalPaid" AS "Amount Paid", rp."Balance", s."CustomerResource"
   FROM "base_SaleOrder" s
   JOIN "base_ResourcePayment" rp ON s."Resource" = rp."DocumentResource"::uuid
   JOIN "base_Guest" g ON g."Resource" = s."CustomerResource"::uuid AND s."Mark" = 'SO'::bpchar
  WHERE s."OrderStatus" <> 6 and s."Balance" > 0
  ORDER BY rp."Id";

ALTER TABLE v_rpt_sale_dept_details OWNER TO postgres;
COMMENT ON VIEW v_rpt_sale_dept_details IS '(POSReport) Get all dept details

Except Paid in full
WHERE s."OrderStatus" != 6;';