EXECUTE spAgregarJuegos 'Anotther', '1990-01-10', 1865.24, 'imagen3.jpg';

EXECUTE spObtenerJuegos;

EXECUTE spEditarJuegos 2, 'ElJuego', '2001-12-1', 1250.98, 'imagen2.png';

EXECUTE spObtenerJuegoPorId 3;

EXECUTE spBorrarJuegos 6;

SELECT * FROM Games;

UPDATE Games SET activo = 1;

EXECUTE sp_helptext spAgregarJuegos;