using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimeController : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public bool idle = false;
    public bool walk = false;
    public bool run = false;
    public bool dance = false;
    private List<Anime> animeList;
    private int id = 0;

    void Start()
    {
        animeList = Enum.GetValues(typeof(Anime)).Cast<Anime>().ToList();
        SetAnime(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            id++;
            if (id >= animeList.Count) id = 0;
            SetAnime(id);
        }
        if (idle) SetAnime((int)Anime.IDLE);
        if (walk) SetAnime((int)Anime.WALK);
        if (run) SetAnime((int)Anime.RUN);
        if (dance) SetAnime((int)Anime.DANCE);
        //
        idle = false;
        walk = false;
        run = false;
        dance = false;
    }

    void SetAnime(int id)
    {
        animator1.SetInteger("id", id);
        animator2.SetInteger("id", id);
    }

}

public enum Anime
{
    IDLE,
    WALK,
    RUN,
    DANCE
}