using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        Mapa mapa = new Mapa(5,5);
        Buscas buscas = new Buscas(mapa);
        Console.WriteLine("Teste");
        
        
        Console.WriteLine("Vizinhos de "+ mapa.GetMapa()[24].GetId());

        foreach(Tile vizinho in mapa.GetMapa()[24].GetVizinhos())
        {
            Console.WriteLine("Vizinho"+ vizinho.GetId());
            
        }
        

        buscas.BuscarEmLargura(mapa.GetMapa()[0],mapa.GetMapa()[4]);
    }
}


