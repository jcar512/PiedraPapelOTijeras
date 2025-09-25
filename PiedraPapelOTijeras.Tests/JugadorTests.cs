using PiedraPapelOTijeras.Dominio;

namespace PiedraPapelOTijeras.Tests
{
  public class JugadorTests
  {
    [Theory]
    [InlineData(5, 5)]
    [InlineData(6, 6)]
    [InlineData(2, 2)]
    [InlineData(10, 10)]
    [InlineData(14, 14)]
    public void IncrementarPuntajeDeAUno(int incrementos, int resultadoEsperado)
    {
      Jugador jugador = new("Juan");

      int esperado = resultadoEsperado;

      for (int i = 0; i < incrementos; i++)
      {
        jugador.IncrementarPuntaje();
      }

      Assert.Equal(esperado, jugador.Puntaje);
    }

    [Fact]
    public void ReiniciarPuntaje()
    {
      Jugador jugador = new("Juan");

      for (int i = 0; i < 5; i++)
      {
        jugador.IncrementarPuntaje();
      }
      jugador.ReiniciarPuntaje();

      Assert.Equal(0, jugador.Puntaje);
    }
  }
}
