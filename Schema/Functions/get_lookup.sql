CREATE OR REPLACE FUNCTION get_lookup (p_table_name VARCHAR) 
 RETURNS TABLE (
 lookup_id INT,
 lookup_name TEXT
) 
AS $$

BEGIN

   
       RETURN QUERY

       EXECUTE('SELECT id, name FROM ' || p_table_name);


END; $$ 
 
LANGUAGE 'plpgsql';

