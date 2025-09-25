using PiedraPapelOTijeras.Dominio;

namespace PiedraPapelOTijeras.Tests
{
    public class JuegoTests
    {
        private readonly Jugador _jugador1;
        private readonly Jugador _jugador2;

        public JuegoTests()
        {
            _jugador1 = new Jugador("Ana");
            _jugador2 = new Jugador("Luis");
        }

        [Theory]
        [InlineData(Juego.Jugada.Piedra, Juego.Jugada.Piedra, null)]
        [InlineData(Juego.Jugada.Papel, Juego.Jugada.Papel, null)]
        [InlineData(Juego.Jugada.Tijeras, Juego.Jugada.Tijeras, null)]
        [InlineData(Juego.Jugada.Tijeras, Juego.Jugada.Papel, "Ana")]
        [InlineData(Juego.Jugada.Tijeras, Juego.Jugada.Piedra, "Luis")]
        [InlineData(Juego.Jugada.Piedra, Juego.Jugada.Papel, "Luis")]
        [InlineData(Juego.Jugada.Piedra, Juego.Jugada.Tijeras, "Ana")]
        public void ChequearJugadas(Juego.Jugada jugada1, Juego.Jugada jugada2, string resultadoEsperado)
        {
            ResultadoRonda resultadoRonda = new(_jugador1, jugada1, _jugador2, jugada2);

            Jugador esperado;

            if (resultadoEsperado == _jugador1.Nombre)
            {
                esperado = _jugador1;
            }
            else if (resultadoEsperado == _jugador2.Nombre)
            {
                esperado = _jugador2;
            }
            else
            {
                esperado = null;
            }

            Assert.Equal(esperado, resultadoRonda.Ganador);
        }

        [Theory]
        [InlineData(1, 3, 3, "Luis")]
        [InlineData(5, 3, 5, "Ana")]
        [InlineData(1, 7, 7, "Luis")]
        [InlineData(9, 5, 9, "Ana")]
        [InlineData(11, 3, 11, "Ana")]
        public void ChequearGanador(int puntosJugador1, int puntosJugador2, int puntosParaGanar, string ganadorEsperado)
        {
          Juego juego = new(_jugador1, _jugador2, puntosParaGanar);

          Jugador esperado = null;

          for (int i = 0; i < puntosJugador1; i++)
          {
          _jugador1.IncrementarPuntaje();
          }

          for (int i = 0; i < puntosJugador2; i++)
          {
          _jugador2.IncrementarPuntaje();
          }

          if (ganadorEsperado == _jugador1.Nombre)
          {
              esperado = _jugador1;
          }
          else if (ganadorEsperado == _jugador2.Nombre)
          {
              esperado = _jugador2;
          }

          Assert.Equal(esperado, juego.ObtenerGanador());
        }
        
        [Theory]
        [InlineData(1, 3, 3, "🎉¡Luis ha ganado la partida! (3-1)")]
        [InlineData(6, 3, 6, "🎉¡Ana ha ganado la partida! (6-3)")]
        [InlineData(4, 5, 5, "🎉¡Luis ha ganado la partida! (5-4)")]
        [InlineData(6, 10, 10, "🎉¡Luis ha ganado la partida! (10-6)")]
        [InlineData(15, 11, 15, "🎉¡Ana ha ganado la partida! (15-11)")]
        public void ChequearTextoGanador(int puntosJugador1, int puntosJugador2, int puntosParaGanar, string textoEsperado)
        {
          Juego juego = new(_jugador1, _jugador2, puntosParaGanar);

          string esperado = textoEsperado;

          for (int i = 0; i < puntosJugador1; i++)
          {
          _jugador1.IncrementarPuntaje();
          }

          for (int i = 0; i < puntosJugador2; i++)
          {
          _jugador2.IncrementarPuntaje();
          }


          Assert.Equal(esperado, juego.ObtenerEstadoJuego());
        }

        [Theory]
        [InlineData(1, 3, 5, "Ana (Puntaje: 1) vs Luis (Puntaje: 3) | Primero en llegar a 5 gana")]
        [InlineData(6, 3, 8, "Ana (Puntaje: 6) vs Luis (Puntaje: 3) | Primero en llegar a 8 gana")]
        [InlineData(4, 5, 9, "Ana (Puntaje: 4) vs Luis (Puntaje: 5) | Primero en llegar a 9 gana")]
        [InlineData(6, 10, 17, "Ana (Puntaje: 6) vs Luis (Puntaje: 10) | Primero en llegar a 17 gana")]
        [InlineData(15, 11, 19, "Ana (Puntaje: 15) vs Luis (Puntaje: 11) | Primero en llegar a 19 gana")]
        public void ChequearPartidaEnCurso(int puntosJugador1, int puntosJugador2, int puntosParaGanar, string textoEsperado)
        {
          Juego juego = new(_jugador1, _jugador2, puntosParaGanar);

          string esperado = textoEsperado;

          for (int i = 0; i < puntosJugador1; i++)
          {
          _jugador1.IncrementarPuntaje();
          }

          for (int i = 0; i < puntosJugador2; i++)
          {
          _jugador2.IncrementarPuntaje();
          }


          Assert.Equal(esperado, juego.ObtenerEstadoJuego());
        }

        [Fact]
        public void ChequearReinicioDePartida()
        {
          Juego juego = new(_jugador1, _jugador2, 5);

          List<ResultadoRonda> rondasEsperadas = [];


          juego.JugarRonda(Juego.Jugada.Tijeras, Juego.Jugada.Papel);

          juego.Reiniciar();

          Assert.Equal(rondasEsperadas, juego.HistorialRondas);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(2)]
        public void ChequearNumeroRondas(int numeroRondas)
        {
          Juego juego = new(_jugador1, _jugador2, numeroRondas);

          int rondasEsperadas = numeroRondas;


          for (int i=0; i<numeroRondas; i++)
          {
          juego.JugarRonda(Juego.Jugada.Tijeras, Juego.Jugada.Tijeras);
          }

          int rondasTotales = juego.HistorialRondas.Count();

          Assert.Equal(rondasEsperadas, rondasTotales);
        }
    }
}
