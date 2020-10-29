using System;
using System.Collections.Generic;
using System.Linq;

class Buscas
{   
    private Mapa _mapa;
    public Buscas (Mapa mapa)
    {
        _mapa = mapa;
    }

    public List<Tile> BuscaEmProfundidade(Tile tile_inicial, Tile tile_final)
    {
        Tile tileAtual = tile_inicial;
        List<Tile> pilha = new List<Tile>();
        List<Tile> explorados = new List<Tile>();
        pilha.Add(tile_inicial);

        while(pilha.Any())
        {
            if(!pilha.Any())
            {
                Console.WriteLine("Nao e possivel encontrar um caminho");
            }

            tileAtual = pilha[pilha.Count-1];
            pilha.RemoveAt(pilha.Count-1);

            explorados.Add(tileAtual);

            if (tileAtual == tile_final)
            {
                return MontaCaminho(tileAtual);
            }else{
                foreach(Tile vizinho in tileAtual.GetVizinhos())
                {
                    if (!explorados.Contains(vizinho) && !pilha.Contains(vizinho))
                    {
                        vizinho.SetPai(tileAtual);
                        pilha.Add(vizinho);
                    }
                }
            }


        }
        Console.WriteLine("Nao e possivel encontrar um caminho");
        return null;
    }
    public List<Tile> BuscarEmLargura(Tile tile_inicial, Tile tile_final)
    {
        Tile tileAtual = tile_inicial;
        List<Tile> fila = new List<Tile>();
        List<Tile> explorados = new List<Tile>();
        fila.Add(tile_inicial);

        while(fila.Any())
        {
            if(!fila.Any())
            {
                Console.WriteLine("Nao e possivel encontrar um caminho");
            }

            tileAtual = fila[0];
            fila.RemoveAt(0);

            explorados.Add(tileAtual);

            if (tileAtual == tile_final)
            {
                return MontaCaminho(tileAtual);
            }else{
                foreach(Tile vizinho in tileAtual.GetVizinhos())
                {
                    if (!explorados.Contains(vizinho) && !fila.Contains(vizinho))
                    {
                        vizinho.SetPai(tileAtual);
                        fila.Add(vizinho);
                    }
                }
            }


        }
        Console.WriteLine("Nao e possivel encontrar um caminho");
        return null;
    }

    public List<Tile> AEstrela (Tile tile_inicial, Tile tile_Objetivo)
    {
        List<Tile> listaAberta = new List<Tile>();
        List<Tile> listaFechada = new List<Tile>();

        bool achouCaminho = false;

        Tile tileAtual = tile_inicial;
        listaAberta.Add(tileAtual);

        while(!achouCaminho)
        {
            tileAtual = ProcurarMenorF(listaAberta);
            listaAberta.Remove(tileAtual);
            listaFechada.Add(tileAtual);

            achouCaminho = tileAtual.Equals(tile_Objetivo);

            foreach(Tile tile in tileAtual.GetVizinhos())
            {
                //verificar se esse tile é uma parede ou
                if(listaFechada.Contains(tile))
                {
                    continue;
                }else{
                    if(!listaAberta.Contains(tile))
                    {
                        listaAberta.Add(tile);
                        tile.SetPai(tileAtual);
                        tile.SetH(CalcularH(tile, tile_Objetivo));
                        tile.SetG(CalcularG(tile,tileAtual));
                        tile.SetF(CalcularF(tile));
                    }else
                    {
                        if(tile.GetG()<tileAtual.GetG())
                        {
                            tile.SetPai(tileAtual);
                            tile.SetG(CalcularG(tileAtual,tile));
                            tile.SetF(CalcularF(tile));
                        }
                    }
                }
            }
            if(!listaAberta.Any())
            {
                Console.WriteLine("Nao e possivel encontrar um caminho");
                return null;
            }
        }
        return MontaCaminho(tileAtual);
    }

    private float CalcularF(Tile tile)
    {
        return tile.GetG()+tile.GetH();
    }

    private float CalcularG(Tile tileAtual, Tile tileVizinho)
    {
        //saber se está na ortogonal tile de 10 por 10, diagonal 14 (hipotenusa)
        if(tileVizinho.GetId()==tileVizinho.GetId()-_mapa.Colunas || tileVizinho.GetId()==tileVizinho.GetId()+_mapa.Colunas || tileVizinho.GetId()==tileVizinho.GetId()-1 ||tileVizinho.GetId()==tileVizinho.GetId()+1)
        {
            return tileVizinho.GetG()+10f;
        }else{
            return tileVizinho.GetG()+14f;
        }
    }

    private Tile ProcurarMenorF(List<Tile> lista)
    {
        lista = lista.OrderBy(tile =>tile.GetF()).ToList();
        return lista[0];
    }

    private float CalcularH(Tile tileAtual, Tile tileVizinho)
    {   //calcular distancia entre 2 pontos
        float posicaoVizinhoX = (float)tileVizinho.GetColuna();
        float posicaoVizinhoY = (float)tileVizinho.GetLinha();

        float posicaoAtualX = (float)tileAtual.GetColuna();
        float posicaoAtualY = (float)tileAtual.GetLinha();

        float distanciaX = Math.Abs(posicaoVizinhoX-posicaoAtualX);
        float distanciaY = Math.Abs(posicaoVizinhoY-posicaoAtualY);

        distanciaX = distanciaX*distanciaX;
        distanciaY = distanciaY*distanciaY;

        double distanciaTotal = Math.Sqrt(distanciaY+distanciaX)*10;
        return (float)distanciaTotal;
    }

    private List<Tile> MontaCaminho(Tile tileAtual)
    {
        List<Tile> listaAuxiliar = new List<Tile>();
        Tile tileAux = tileAtual;
        while (tileAux!=null)
        {
            listaAuxiliar.Add(tileAux);
            tileAux = tileAux.GetPai();
        }

        listaAuxiliar.Reverse();
        Console.WriteLine("Caminho:");

        foreach(Tile tile in listaAuxiliar)
        {
            Console.WriteLine("Tile: "+ tile.GetId());
        }

        return listaAuxiliar;
    }
}