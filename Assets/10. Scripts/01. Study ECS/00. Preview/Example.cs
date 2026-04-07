using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ECS_Preview.OOP
{
    public class Stat
    {
        public int Atk;
        public int Def;
        public int Hp;
    }

    public class Info
    {
        public string Name;
        public string Element;
        public string Region;
    }
    
    public class Enemy
    {
        public Stat Stat { get; set; }
        public Info Info { get; set; }

        public void TakeDamage(int damage) { }
    }

    public class EnemyManager
    {
        public Dictionary<Collider, Enemy> Enemies { get; set; }
        public List<Enemy> EnemyList { get; set; }
    }
}

namespace ECS_Preview.DOP
{
    public struct Stat
    {
        
    }
    
    public struct Info
    {
        
    }

    public struct EnemySystem
    {
        private Stat[] Stats { get; set; }
        private Info[] Infos { get; set; }
        private Collider[] Colliders { get; set; }

        public Stat GetEnemyStat(Collider collider)
        {
            int entity = GetEntity(collider);
            if (entity == -1) return default;
            return Stats[entity];
        }

        private int GetEntity(Collider collider)
        {
            int index = -1;
            
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (collider == Colliders[i]) index = i;
            }
            
            return index;
        }
    }
}



