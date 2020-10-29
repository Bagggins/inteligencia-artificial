using System;
using System.Collections.Generic;

class Mapa
{
    private List<Tile> _mapa = new List<Tile>();
    private int _linhas;
    private int _colunas;

    public Mapa(int linhas, int colunas)
    {
        _linhas = linhas; 
        _colunas = colunas;
        CriaMapa();
        ConfiguraMapa();
    }

    public List<Tile> GetMapa()
    {
        return _mapa;
    }

    private void ConfiguraMapa()
    {
        foreach(Tile tile in _mapa)
        {
            AcharCantos(tile);
            AcharOrtogonais(tile);
        }
    }

    private void AcharOrtogonais(Tile tile)
    {
        int id = tile.GetId();
        List<Tile> ortogonais = new List<Tile>();
        //vizinho na esquerda
        if(tile.GetColuna()>0)
        {
            int aux = id -1;
            ortogonais.Add(_mapa[aux]);
        }
        //direita ok
        if(tile.GetColuna()<_colunas-1)
        {
            int aux = id +1;
            ortogonais.Add(_mapa[aux]);
        }
    	//vizinho de cima
        if(tile.GetLinha()>0)
        {
            int aux = id -_colunas;
            ortogonais.Add(_mapa[aux]);
        }

        if(tile.GetLinha()<_linhas-1)
        {
            int aux = id +_colunas;
            ortogonais.Add(_mapa[aux]);
        }

        foreach(Tile vizinho in ortogonais)
        {
            tile.AdicionaVizinho(vizinho);
        }
    }

    private void AcharCantos(Tile tile)
    {
        int id = tile.GetId();
        List<Tile> cantos = new List<Tile>();

        // calcular linha e coluna
        //int linhaDoTile = (tile.GetId()/_linhas)+1;
        //int colunaDoTile = (tile.GetId()%_colunas)+1;

        // canto superior esquerda
        if(tile.GetLinha()>0 && tile.GetColuna()>0)
        {
            int aux = id -_colunas-1;
            cantos.Add(_mapa[aux]);
        }
        // canto superior direita
        if(tile.GetLinha()>0 && tile.GetColuna()<_colunas-1)
        {
            int aux = id -_colunas+1;
            cantos.Add(_mapa[aux]);
        }
        // canto inferior esquerdo ok
        if(tile.GetLinha()<_linhas-1 && tile.GetColuna() > 0)
        {
            int aux = id +_colunas-1;
            cantos.Add(_mapa[aux]);
            
        }

        //canto inferior direita ok
        if(tile.GetLinha()<_linhas-1 && tile.GetColuna() <_colunas-1)
        {
            int aux = id +_colunas+1;
            cantos.Add(_mapa[aux]);
        }

        foreach(Tile vizinho in cantos)
        {
            tile.AdicionaVizinho(vizinho);
        }

    }

    private void CriaMapa()
    {
        int contador = 0;
        for (int i = 0; i<_linhas; i++)
        {
            for (int j = 0; j<_colunas; j++)
            {
                
                Tile tile = new Tile(contador,i,j);
                _mapa.Add(tile);
                contador++;
            }
        }
    }
}