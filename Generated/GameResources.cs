using UnityEngine;
using UnityEngine.UI;

// This file is auto-generated. Do not modify manually.

public static class GameResources
{
    public static class Animation
    {
        public static class Coins
        {
        }
        public static class Enemy
        {
        }
        public static class Menu
        {
            public static class Class
            {
            }
        }
        public static class NPC
        {
        }
        public static class Player
        {
            public static class Attack
            {
                public static class Bow_Attack_Idle
                {
                }
                public static class Close_Combat_Idle
                {
                }
                public static class Club_Attack_Idle
                {
                }
                public static class Magic_Staff_Attack_Idle
                {
                }
                public static class Sword_Attack_Idle
                {
                }
            }
            public static class Use_Consumable_Item
            {
            }
        }
        public static class User_Interface
        {
        }
    }
    public static class Arrow_Prefabs
    {
        public static Arrow Arrow => Resources.Load<Arrow>("Arrow Prefabs/Arrow");
        public static Vanishing Fireball => Resources.Load<Vanishing>("Arrow Prefabs/Fireball");
    }
    public static class Font
    {
        public static Material ofont => Resources.Load<Material>("Font/ofont");
    }
    public static class Item_Prefab
    {
        public static class Armor
        {
            public static Item Skin_Boots_1 => Resources.Load<Item>("Item Prefab/Armor/Skin Boots 1");
            public static Item Skin_Boots_2 => Resources.Load<Item>("Item Prefab/Armor/Skin Boots 2");
            public static Item Skin_Boots_3 => Resources.Load<Item>("Item Prefab/Armor/Skin Boots 3");
            public static Item Skin_Boots => Resources.Load<Item>("Item Prefab/Armor/Skin Boots");
            public static Item Skin_Breastplate_1 => Resources.Load<Item>("Item Prefab/Armor/Skin Breastplate 1");
            public static Item Skin_Breastplate_2 => Resources.Load<Item>("Item Prefab/Armor/Skin Breastplate 2");
            public static Item Skin_Breastplate_3 => Resources.Load<Item>("Item Prefab/Armor/Skin Breastplate 3");
            public static Item Skin_Breastplate => Resources.Load<Item>("Item Prefab/Armor/Skin Breastplate");
            public static Item Skin_Helmet_1 => Resources.Load<Item>("Item Prefab/Armor/Skin Helmet 1");
            public static Item Skin_Helmet_2 => Resources.Load<Item>("Item Prefab/Armor/Skin Helmet 2");
            public static Item Skin_Helmet_3 => Resources.Load<Item>("Item Prefab/Armor/Skin Helmet 3");
            public static Item Skin_Helmet => Resources.Load<Item>("Item Prefab/Armor/Skin Helmet");
        }
        public static Item Apple => Resources.Load<Item>("Item Prefab/Apple");
        public static Item Arrow_Item => Resources.Load<Item>("Item Prefab/Arrow Item");
        public static Item Fireball_Item => Resources.Load<Item>("Item Prefab/Fireball Item");
        public static Item Magic_Staff => Resources.Load<Item>("Item Prefab/Magic Staff");
        public static Item Magic_Sword => Resources.Load<Item>("Item Prefab/Magic Sword");
        public static Item Potion_Health_Big => Resources.Load<Item>("Item Prefab/Potion Health Big");
        public static Item Potion_Health_Small => Resources.Load<Item>("Item Prefab/Potion Health Small");
        public static Item Potion_Mana_Big => Resources.Load<Item>("Item Prefab/Potion Mana Big");
        public static Item Potion_Mana_Small => Resources.Load<Item>("Item Prefab/Potion Mana Small");
        public static Item Silver_Sword_1 => Resources.Load<Item>("Item Prefab/Silver Sword 1");
        public static Item Silver_Sword_2 => Resources.Load<Item>("Item Prefab/Silver Sword 2");
        public static Item Silver_Sword_3 => Resources.Load<Item>("Item Prefab/Silver Sword 3");
        public static Item Silver_Sword => Resources.Load<Item>("Item Prefab/Silver Sword");
        public static Item Stick => Resources.Load<Item>("Item Prefab/Stick");
        public static Item Wooden_Bow__1_ => Resources.Load<Item>("Item Prefab/Wooden Bow (1)");
        public static Item Wooden_Bow => Resources.Load<Item>("Item Prefab/Wooden Bow");
        public static Item Wooden_Club_1 => Resources.Load<Item>("Item Prefab/Wooden Club 1");
        public static Item Wooden_Club => Resources.Load<Item>("Item Prefab/Wooden Club");
        public static Item Wooden_Dagger => Resources.Load<Item>("Item Prefab/Wooden Dagger");
    }
    public static class ItemObject
    {
    }
    public static class Prefabs
    {
        public static class Coins
        {
            public static Coin Parent_Copper_Coin => Resources.Load<Coin>("Prefabs/Coins/Parent Copper Coin");
            public static Coin Parent_Gold_Coin => Resources.Load<Coin>("Prefabs/Coins/Parent Gold Coin");
            public static Coin Parent_Silver_Coin => Resources.Load<Coin>("Prefabs/Coins/Parent Silver Coin");
        }
        public static class Enemy
        {
            public static AppleEnemy Enemy_Apple => Resources.Load<AppleEnemy>("Prefabs/Enemy/Enemy Apple");
            public static DamageHandlerEnemy Enemy_Rabbit => Resources.Load<DamageHandlerEnemy>("Prefabs/Enemy/Enemy Rabbit");
        }
        public static class NPC
        {
            public static Image Button_Buy_NPC => Resources.Load<Image>("Prefabs/NPC/Button Buy NPC");
            public static NPC_QUEST NPC_Quests => Resources.Load<NPC_QUEST>("Prefabs/NPC/NPC Quests");
            public static NPC_Shop NPC_Shop => Resources.Load<NPC_Shop>("Prefabs/NPC/NPC Shop");
        }
        public static class Quest
        {
            public static Image Prefab_Quest_Info => Resources.Load<Image>("Prefabs/Quest/Prefab Quest Info");
        }
        public static GameObject Anvil => Resources.Load<GameObject>("Prefabs/Anvil");
        public static Vanishing Area_Crack => Resources.Load<Vanishing>("Prefabs/Area Crack");
        public static CanvasScaler Health => Resources.Load<CanvasScaler>("Prefabs/Health");
        public static Image Panel_Info_Save => Resources.Load<Image>("Prefabs/Panel Info Save");
        public static Image Slot => Resources.Load<Image>("Prefabs/Slot");
        public static GameObject Tree => Resources.Load<GameObject>("Prefabs/Tree");
    }
    public static class Sounds
    {
        public static AudioClip EatApple => Resources.Load<AudioClip>("Sounds/EatApple");
        public static AudioClip ShootFromBow => Resources.Load<AudioClip>("Sounds/ShootFromBow");
        public static AudioClip StepPlayer1 => Resources.Load<AudioClip>("Sounds/StepPlayer1");
        public static AudioClip StepPlayer2 => Resources.Load<AudioClip>("Sounds/StepPlayer2");
        public static AudioClip StepPlayer3 => Resources.Load<AudioClip>("Sounds/StepPlayer3");
        public static AudioClip TakeItem => Resources.Load<AudioClip>("Sounds/TakeItem");
        public static AudioClip TakeMoney => Resources.Load<AudioClip>("Sounds/TakeMoney");
        public static AudioClip TensionOfTheBowstring => Resources.Load<AudioClip>("Sounds/TensionOfTheBowstring");
        public static AudioClip WooshSword => Resources.Load<AudioClip>("Sounds/WooshSword");
    }
    public static class Tilemap
    {
        public static GameObject Location__Palette => Resources.Load<GameObject>("Tilemap/Location  Palette");
    }
}
