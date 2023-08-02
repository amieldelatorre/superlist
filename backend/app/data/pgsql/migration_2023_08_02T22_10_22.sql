SELECT * FROM pg_catalog.pg_tables;

BEGIN;

    CREATE TABLE IF NOT EXISTS vlist (
        id                  SERIAL PRIMARY KEY
        ,uuid               VARCHAR(32) NOT NULL UNIQUE
        ,title              varchar(1000)
        ,description        varchar(1000)
        ,created_by         varchar(1000)
        ,updated_by         varchar(1000)
        ,passphrase         varchar(1000)
        ,datetime_created   TIMESTAMPTZ NOT NULL
        ,datetime_updated   TIMESTAMPTZ NOT NULL
        ,expiry             TIMESTAMPTZ NOT NULL
    );

    CREATE TABLE IF NOT EXISTS vlist_item (
        id                  SERIAL PRIMARY KEY
		,vlist_id			INTEGER
        ,uuid               VARCHAR(32) NOT NULL UNIQUE
        ,datetime_created   TIMESTAMPTZ NOT NULL
        ,datetime_updated   TIMESTAMPTZ NOT NULL
        ,created_by         varchar(1000)
        ,updated_by         varchar(1000)
        ,item_value         varchar(1000)
        ,is_completed       boolean DEFAULT false
		,CONSTRAINT fk_vlist
			FOREIGN KEY(vlist_id)
				REFERENCES vlist(id)
    );

-- ROLLBACK;
COMMIT;

