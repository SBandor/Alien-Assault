public static class DBManager
{
    public static string username;
    public static int logros;
    public static int actualpartidaID;
    public static int actualpartidaOleada=1;

    public static int partidaIDA;
    public static int rangoA;
    public static int killScoreA;
    public static int maxScoreA;
    public static int oleadaA;

    
    public static int partidaIDB;
    public static int rangoB;
    public static int killScoreB;
    public static int maxScoreB;
    public static int oleadaB;

   
    public static int partidaIDC;
    public static int rangoC;
    public static int killScoreC;
    public static int maxScoreC;
    public static int oleadaC;

    public static bool LoggedIn { get { return username != null ; } }

    public static void LoggedOut()
    {
        username=null;
    }

    public static bool DentroDePartida { get { return actualpartidaID != 0 ; } }

    public static void FueraDePartida()
    {
        actualpartidaID=0;
    }  
}
