CREATE OR ALTER PROCEDURE spAgregarJuegos  
@nombre VARCHAR(100),
@fechaLanzamiento DATE,
@precio DECIMAL(18,2),
@imagen VARCHAR(100)
AS 
BEGIN 
	INSERT INTO Games (nombre, fechaLanzamiento, precio, imagen, activo)
	VALUES (@nombre, @fechaLanzamiento, @precio, @imagen, 1);
END;

CREATE OR ALTER PROC spObtenerJuegos
AS
BEGIN 
	SELECT idVideojuego, nombre, fechaLanzamiento, precio, imagen, activo
	FROM Games
	WHERE activo = 1;
END;

CREATE OR ALTER PROC spEditarJuegos
@idVideojuego INT,
@nombre VARCHAR(100),
@fechaLanzamiento DATE,
@precio DECIMAL(18,2),
@imagen VARCHAR(100)
AS 
BEGIN
	UPDATE Games SET nombre = @nombre, fechaLanzamiento = @fechaLanzamiento, precio = @precio, imagen = @imagen
	WHERE idVideojuego = @idVideojuego;
END;

CREATE OR ALTER PROC spObtenerJuegoPorId
@idVideojuego INT
AS
BEGIN
	SELECT idVideojuego, nombre, fechaLanzamiento, precio, imagen, activo
	FROM Games
	WHERE idVideojuego = @idVideojuego;
END;

CREATE OR ALTER PROC spBorrarJuegos
@idVideojuego INT
AS
BEGIN 
	UPDATE Games SET activo = 0 WHERE idVideojuego = @idVideojuego;
END;
