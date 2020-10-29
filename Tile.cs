using System;
using System.Collections.Generic;

class Tile
{
        private int _id, _linha, _coluna;
        private List<Tile> _vizinhos = new List<Tile>();

        private Tile _pai = null;

        public Tile(int id, int linha, int coluna)
        {
            _id = id;
            _linha = linha;
            _coluna = coluna;
        }
        public void SetPai(Tile pai)
        {
            _pai = pai;
        }
        public Tile GetPai ()
        {
            return _pai;
        }
        public void AdicionaVizinho(Tile vizinho)
        {
            _vizinhos.Add(vizinho);
        }

        public List<Tile> GetVizinhos()
        {
            return _vizinhos;
        }
        public int GetId() 
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public int GetLinha() 
        {
            return _linha;
        }

        public void SetLinha(int linha)
        {
            _linha = linha;
        }

        public int GetColuna() 
        {
            return _coluna;
        }

        public void SetColuna(int coluna)
        {
            _coluna = coluna;
        }

}