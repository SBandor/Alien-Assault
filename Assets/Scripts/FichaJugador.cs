using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaJugador
{
    public string jugador;
    public int rango;
    public int maxScore;
    public int maxOleadas;


    public FichaJugador(string j, int r, int ms, int mo)
    {
        this.jugador=j;
        this.rango=r;
        this.maxScore=ms;
        this.maxOleadas=mo;
    }

    public void SetJugador(string j){jugador=j;}
    public string GetJugador(){return jugador;}

    public void SetRango(int i){rango=i;}
    public int GetRango(){return rango;}

    public void SetMaxScore(int i){maxScore=i;}
    public int GetMaxScore(){return rango;}

    public void SetMaxOleadas(int i){maxOleadas=i;}
    public int GetMaxOleadas(){return maxOleadas;}
}
