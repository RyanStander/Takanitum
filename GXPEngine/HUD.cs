using System;
using System.Drawing;
using GXPEngine;

public class HUD : Canvas
{

    private Player _player1Dummy;
    private Player _player2Dummy;
    private int _amountOfLives1;
    private int _amountOfLives2;
    private bool _gameOver=false;


    //------------------------------------------------
    //                  Player()    
    //------------------------------------------------
    public HUD(Player player1, Player player2) : base(1980, 128)
    {
        SetScaleXY(2);
        _player1Dummy = player1;
        _player2Dummy = player2;
    }


    //------------------------------------------------
    //                  Update()    
    //------------------------------------------------
    void Update()
    {
        _amountOfLives1 = _player1Dummy.GetLives();
        _amountOfLives2 = _player2Dummy.GetLives();
        GameOverPlayer1();
        GameOverPlayer2();
    }

    //------------------------------------------------
    //                  GameOverPlayer1()    
    //------------------------------------------------
    public void GameOverPlayer1()
    {
        if (_amountOfLives1 == 0)
        {
            graphics.DrawString("Player 1 has Lost.", SystemFonts.DefaultFont, Brushes.Red, 425, 100);
            graphics.DrawString("Press 'SPACE' to reset the game.", SystemFonts.DefaultFont, Brushes.Red, 425, 110);
        }
    }

    //------------------------------------------------
    //                  GameOverPlayer2()    
    //------------------------------------------------
    public void GameOverPlayer2()
    {
        if (_amountOfLives2 == 0)
        {
            graphics.DrawString("Player 2 has Lost.", SystemFonts.DefaultFont, Brushes.Red, 425, 100);
            graphics.DrawString("Press 'SPACE' to reset the game.", SystemFonts.DefaultFont, Brushes.Red, 425, 110);
        }
    }

    //------------------------------------------------
    //                  GetGameOver()    
    //------------------------------------------------
    public bool GetGameOver()
    {
        return _gameOver;
    }

}
