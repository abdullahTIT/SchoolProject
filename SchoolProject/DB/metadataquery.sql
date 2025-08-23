SELECT 
    TABLE_SCHEMA, 
    TABLE_NAME 
FROM INFORMATION_SCHEMA.VIEWS;


SELECT 
    v.name AS ViewName,
    m.definition AS ViewDefinition
FROM 
    sys.views v
JOIN 
    sys.sql_modules m ON v.object_id = m.object_id
ORDER BY 
    v.name;
go

SELECT name 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('StudentFees');
-- 1. عرض جميع الجداول مع عدد الأعمدة
SELECT 
    t.name AS TableName,
    s.name AS SchemaName,
    COUNT(c.column_id) AS ColumnCount
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
LEFT JOIN sys.columns c ON t.object_id = c.object_id
GROUP BY t.name, s.name
ORDER BY s.name, t.name;

-- 2. عرض جميع الـ Triggers مع الجدول المرتبط
SELECT 
    tr.name AS TriggerName,
    s.name AS SchemaName,
    tbl.name AS TableName,
    tr.is_disabled,
    tr.is_instead_of_trigger,
    tr.create_date,
    tr.modify_date
FROM sys.triggers tr
INNER JOIN sys.tables tbl ON tr.parent_id = tbl.object_id
INNER JOIN sys.schemas s ON tbl.schema_id = s.schema_id
ORDER BY s.name, tbl.name, tr.name;

-- 3. عرض جميع الإجراءات المخزنة (Stored Procedures)
SELECT 
    p.name AS ProcedureName,
    s.name AS SchemaName,
    p.create_date,
    p.modify_date
FROM sys.procedures p
INNER JOIN sys.schemas s ON p.schema_id = s.schema_id
ORDER BY s.name, p.name;

-- 4. عرض جميع الـ Views
SELECT 
    v.name AS ViewName,
    s.name AS SchemaName,
    v.create_date,
    v.modify_date
FROM sys.views v
INNER JOIN sys.schemas s ON v.schema_id = s.schema_id
ORDER BY s.name, v.name;

-- 5. عرض جميع الأعمدة لكل جدول مع نوع البيانات
SELECT 
    s.name AS SchemaName,
    t.name AS TableName,
    c.name AS ColumnName,
    ty.name AS DataType,
    c.max_length,
    c.is_nullable,
    c.is_identity
FROM sys.columns c
INNER JOIN sys.tables t ON c.object_id = t.object_id
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
INNER JOIN sys.types ty ON c.user_type_id = ty.user_type_id
ORDER BY s.name, t.name, c.column_id;

-- 6. عرض Triggers الخاصة بجدول معين (استبدل 'YourTableName' باسم الجدول)
DECLARE @TableName NVARCHAR(128) = 'YourTableName';

SELECT 
    tr.name AS TriggerName,
    s.name AS SchemaName,
    tbl.name AS TableName,
    tr.is_disabled,
    tr.is_instead_of_trigger,
    tr.create_date,
    tr.modify_date
FROM sys.triggers tr
INNER JOIN sys.tables tbl ON tr.parent_id = tbl.object_id
INNER JOIN sys.schemas s ON tbl.schema_id = s.schema_id
WHERE tbl.name = @TableName
ORDER BY tr.name;

-- 7. عرض محتوى (تعريف) Stored Procedure (استبدل 'ProcedureName')
SELECT OBJECT_DEFINITION(OBJECT_ID('ProcedureName')) AS ProcedureDefinition;

-- 8. عرض محتوى (تعريف) Trigger (استبدل 'TriggerName')
SELECT OBJECT_DEFINITION(OBJECT_ID('TriggerName')) AS TriggerDefinition;

-- 9. عرض محتوى (تعريف) View (استبدل 'ViewName')
SELECT OBJECT_DEFINITION(OBJECT_ID('ViewName')) AS ViewDefinition;

-- 10. عرض هيكل الأعمدة لجدول معين (استبدل 'TableName')
EXEC sp_columns 'TableName';

-- 11. عرض الأعمدة بالتفصيل من INFORMATION_SCHEMA
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'TableName'
ORDER BY ORDINAL_POSITION;

-- 12. عرض بيانات من جدول (استبدل 'TableName' وعدد السجلات)
SELECT TOP 100 * FROM TableName;

-- 13. عرض محتوى أي كائن برمجي (Procedure, Trigger, View) (استبدل 'ObjectName')
SELECT 
    o.name AS ObjectName,
    o.type_desc AS ObjectType,
    m.definition AS ObjectDefinition
FROM sys.objects o
JOIN sys.sql_modules m ON o.object_id = m.object_id
WHERE o.name = 'ObjectName';

-- 14. عرض كل الإجراءات، التريجرات، والفيوهات مع محتواها
SELECT 
    o.name AS ObjectName,
    CASE o.type
        WHEN 'P' THEN 'Stored Procedure'
        WHEN 'TR' THEN 'Trigger'
        WHEN 'V' THEN 'View'
        ELSE o.type_desc
    END AS ObjectType,
    m.definition AS ObjectDefinition
FROM sys.objects o
INNER JOIN sys.sql_modules m ON o.object_id = m.object_id
WHERE o.type IN ('P', 'TR', 'V')
ORDER BY ObjectType, ObjectName;

-- 15. جمع محتويات جميع الإجراءات، التريجرات، والفيوهات في عمود واحد (Box1) نص ضخم (SQL Server 2017+)
SELECT 
    STRING_AGG(CONCAT('/* ', o.name, ' [', 
                      CASE o.type
                        WHEN 'P' THEN 'Stored Procedure'
                        WHEN 'TR' THEN 'Trigger'
                        WHEN 'V' THEN 'View'
                        ELSE o.type_desc END, 
                      ' ] */', CHAR(13) + CHAR(10),
                      m.definition), CHAR(13) + CHAR(10) + '------------------' + CHAR(13) + CHAR(10)) 
        WITHIN GROUP (ORDER BY o.type, o.name) AS Box1
FROM sys.objects o
INNER JOIN sys.sql_modules m ON o.object_id = m.object_id
WHERE o.type IN ('P', 'TR', 'V');







SELECT
    con.name AS ConstraintName,
    con.type_desc AS ConstraintType,
    col.name AS ColumnName,
    tbl.name AS TableName
FROM 
    sys.objects con
INNER JOIN 
    sys.columns col ON col.object_id = con.parent_object_id
INNER JOIN 
    sys.tables tbl ON tbl.object_id = con.parent_object_id
WHERE 
    tbl.name = 'StudentFees'
    AND col.name = 'Status'
    AND con.type IN ('C', 'D', 'F', 'PK', 'UQ')  
	-- C=Check, D=Default, F=Foreign Key, PK=Primary Key, UQ=Unique
