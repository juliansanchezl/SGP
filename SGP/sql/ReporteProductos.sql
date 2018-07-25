DROP PROCEDURE SP_ReporteProductos;
CREATE PROCEDURE SP_ReporteProductos
--[Parameters]
AS
BEGIN
select p.id,p.nombre, TP.nombre as tipo, p.descripcion, precio from Producto P 
INNER JOIN TipoProducto TP ON p.tipoproductoid=TP.id;
END;

DROP PROCEDURE SP_ReporteProductosByID;
CREATE PROCEDURE SP_ReporteProductosByID
	--[Parameters]
	@tipoproductoid INT
AS
BEGIN
	select p.id,p.nombre, TP.nombre as tipo, p.descripcion, precio from Producto P 
	INNER JOIN TipoProducto TP ON p.tipoproductoid=TP.id
	WHERE P.tipoproductoid=@tipoproductoid;
END;

DROP PROCEDURE SP_ReporteClientesByCiudad;
CREATE PROCEDURE SP_ReporteClientesByCiudad
@nombreciudad NVARCHAR(200)
AS
BEGIN

SELECT P.nombres, p.apellidos, C.nombre as ciudad, D.nombre as departamento, Pais.nombre pais FROM Persona P
INNER JOIN Ciudad C on p.ciudadid=C.id
INNER JOIN Departamento D ON c.departamentoid=d.id
INNER JOIN Pais on D.paisid=Pais.id
where C.nombre like '%' + @nombreciudad +'%';

END;


--Invocar StoreProcedure
EXEC SP_ReporteProductos;
EXEC SP_ReporteProductosByID 1;
EXEC SP_ReporteClientesByCiudad 'tunja';

delete from pago;


select * from venta
update venta set clienteid=null;


select * from Persona;
delete from Persona;


