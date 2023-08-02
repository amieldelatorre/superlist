SELECT 
	table_name
	,column_name
	,data_type
FROM
	information_schema.columns
WHERE
	table_name in (
		SELECT tablename
		FROM pg_catalog.pg_tables
		WHERE schemaname != 'pg_catalog'
		AND schemaname != 'information_schema'
	)
ORDER BY
	table_name ASC;