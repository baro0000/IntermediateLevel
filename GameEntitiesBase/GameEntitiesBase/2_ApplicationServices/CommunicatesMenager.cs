namespace GameEntitiesBase._2_ApplicationServices
{
    public static class CommunicatesMenager
    {
        private static List<string> attackParriedByPlayerDescription = new List<string>()
        {
            "You deflected the incoming blow.",
            "You swiftly, parried the attack.",
            "With skillful precision, you blocked the oncoming strike.",
            "You countered the opponent's move with a well-timed parry.",
            "Evading the attack, you swiftly deflected the strike.",
            "Using your sword, you blocked the opponent's incoming blow.",
            "You skillfully warded off the opponent's attack.",
            "Reacting quickly, you parried the incoming strike.",
            "In a defensive stance, you skillfully blocked the enemy's blow.",
            "With a quick maneuver, you deflected the oncoming attack.",
            "You intercepted the opponent's strike with a swift parry.",
            "Anticipating the attack, you expertly parried the blow.",
            "With a precise movement, you blocked the enemy's strike.",
            "Reacting swiftly, you countered the opponent's attack with a parry."
        };

        private static List<string> attackParriedByNpcDescription = new List<string>()
        {
            "The NPC deflects the incoming blow with agility.",
            "Swiftly, the NPC parries the attack.",
            "With skillful precision, the NPC blocks the oncoming strike.",
            "The NPC counters the opponent's move with a well-timed parry.",
            "Evading the attack, the NPC swiftly deflects the strike.",
            "Using its shield, the NPC blocks the opponent's incoming blow.",
            "The NPC skillfully wards off the opponent's attack.",
            "Reacting quickly, the NPC parries the incoming strike.",
            "Dodging to the side, the NPC deflects the attack.",
            "In a defensive stance, the NPC skillfully blocks the enemy's blow.",
            "With a quick maneuver, the NPC deflects the oncoming attack.",
            "The NPC intercepts the opponent's strike with a swift parry.",
            "Anticipating the attack, the NPC expertly parries the blow.",
            "With a precise movement, the NPC blocks the enemy's strike.",
            "Reacting swiftly, the NPC counters the opponent's attack with a parry."
        };

        private static List<string> playerDamageDescriptions = new List<string>()
        {
            "You stagger back as the enemy's strike lands.",
            "With a grunt of pain, you feel the impact of the blow.",
            "You wince as the enemy's attack connects.",
            "Feeling the force of the blow, your defenses falter.",
            "You grit your teeth as the enemy's strike hits home.",
            "A pained expression crosses your face as you take the hit.",
            "Your armor rattles from the force of the enemy's blow.",
            "Taking the hit head-on, you feel the sting of the attack.",
            "With a groan, you recoil from the impact of the strike.",
            "Your defenses crumble as you suffer the enemy's attack.",
            "A grimace of pain twists your features as you are struck.",
            "You stumble backward, reeling from the force of the blow.",
            "Injured, you grimly endure the pain of the enemy's attack.",
            "Your resolve wavers as you suffer the enemy's assault.",
            "Struck squarely, you feel the full force of the blow.",
            "With a cry of pain, you absorb the impact of the strike.",
            "Your armor rings with the force of the enemy's blow.",
            "Battered and bruised, you struggle to maintain your footing.",
            "You grapple with the pain of the enemy's powerful strike.",
            "Shaken but undeterred, you brace against the enemy's assault."
        };

        private static List<string> npcDamageDescriptions = new List<string>()
            {
            "The NPC staggers back as your strike lands.",
            "With a grunt of pain, the NPC feels the impact of your blow.",
            "The NPC winces as your attack connects.",
            "Feeling the force of your blow, the NPC's defenses falter.",
            "The NPC grits their teeth as your strike hits home.",
            "A pained expression crosses the NPC's face as they take the hit.",
            "Their armor rattles from the force of your blow.",
            "Taking the hit head-on, the NPC feels the sting of your attack.",
            "With a groan, the NPC recoils from the impact of your strike.",
            "Their defenses crumble as they suffer your attack.",
            "A grimace of pain twists the NPC's features as they are struck.",
            "The NPC stumbles backward, reeling from the force of your blow.",
            "Injured, the NPC grimly endures the pain of your attack.",
            "Their resolve wavers as they suffer your assault.",
            "Struck squarely, the NPC feels the full force of your blow.",
            "With a cry of pain, the NPC absorbs the impact of your strike.",
            "Their armor rings with the force of your blow.",
            "Battered and bruised, the NPC struggles to maintain their footing.",
            "They grapple with the pain of your powerful strike.",
            "Shaken but undeterred, the NPC braces against your assault."
        };

        private static List<string> playerDodgingDescriptions = new List<string>()
        {
            "You deftly sidestep the incoming blow, narrowly avoiding it.",
            "With a swift movement, you duck under the attack, avoiding harm.",
            "Anticipating the strike, you quickly step back, dodging out of harm's way.",
            "Your quick reflexes allow you to evade the attack effortlessly.",
            "You smoothly pivot away from the incoming strike, avoiding contact.",
            "A nimble dodge allows you to evade the attack with ease.",
            "You instinctively lean back, narrowly avoiding the incoming blow.",
            "Reacting swiftly, you jump backward, evading the attack entirely.",
            "You step to the side at the last moment, narrowly dodging the strike.",
            "With a quick roll, you evade the attack, staying one step ahead.",
            "You twist away from the incoming strike, avoiding the brunt of it.",
            "Swift footwork allows you to dance away from the impending blow.",
            "Your agile movements allow you to slip past the attack unscathed.",
            "You lean to the side, narrowly dodging the strike with grace.",
            "With a deft maneuver, you sidestep the incoming attack, avoiding injury."
        };

        private static List<string> npcEvadingDescriptions = new List<string>()
        {
            "The NPC quickly steps back, narrowly avoiding the incoming blow.",
            "With a sudden retreat, the NPC manages to evade the attack at the last moment.",
            "Reacting swiftly, the NPC leaps backward, narrowly dodging the strike.",
            "Anticipating the attack, the NPC jumps out of harm's way, narrowly avoiding the blow.",
            "The NPC instinctively backs away, narrowly dodging the incoming strike.",
            "With a quick sidestep, the NPC evades the attack, staying out of harm's reach.",
            "The NPC swiftly retreats, narrowly dodging the impending blow.",
            "Reacting with agility, the NPC ducks and weaves, evading the attack with ease.",
            "The NPC deftly maneuvers away from the incoming strike, avoiding contact.",
            "With a quick dodge, the NPC evades the attack, sidestepping the blow.",
            "The NPC quickly steps aside, narrowly avoiding the brunt of the attack.",
            "In a sudden move, the NPC leaps away from the impending strike, avoiding injury.",
            "With nimble footwork, the NPC dances away from the incoming blow, staying safe.",
            "The NPC sidesteps the attack with grace, narrowly avoiding being hit.",
            "Reacting swiftly, the NPC evades the strike, stepping back to safety."
        };

        private static List<string> playerFinisherDescriptions = new List<string>()
        {
            "With a mighty swing, you cleave through the NPC's defenses, delivering a fatal blow.",
            "Channeling your inner strength, you unleash a devastating strike, ending the NPC's life.",
            "You perform a flawless combo, finishing off the NPC with a series of precise attacks.",
            "In a display of skill and power, you execute a deadly finisher, defeating the NPC.",
            "With a swift motion, you deliver a fatal blow, bringing the NPC to their knees.",
            "Summoning your courage, you strike true, delivering a finishing blow to the NPC.",
            "With unwavering determination, you deliver a final, fatal strike to the NPC.",
            "You unleash a flurry of strikes, overwhelming the NPC and sealing their fate.",
            "In a stunning display of skill, you execute a devastating finisher, ending the NPC's life.",
            "With a precise strike, you deliver a fatal blow, ending the NPC's existence.",
            "Your relentless assault leaves the NPC defenseless, ultimately leading to their demise.",
            "In a swift and decisive move, you deliver a finishing blow, ending the NPC's suffering.",
            "With a swift motion, you strike a fatal blow, bringing the NPC's life to an end.",
            "Your final strike is swift and merciless, ending the NPC's life in an instant.",
            "With unmatched skill, you deliver a finishing blow, putting an end to the NPC's reign of terror.",
            "In a display of sheer power, you deliver a devastating finishing blow, ending the NPC's life.",
            "With a single, well-placed strike, you end the NPC's existence, leaving no room for mercy.",
            "Your precision strikes leave the NPC vulnerable, allowing for a fatal finishing blow.",
            "With a powerful roar, you deliver a crushing blow, ending the NPC's life in one swift motion.",
            "In a moment of triumph, you deliver a finishing blow, putting an end to the NPC's resistance."
        };

        private static List<string> npcFinisherDescriptions = new List<string>()
        {
            "With a sudden burst of energy, the NPC delivers a devastating blow, ending your life.",
            "You watch in horror as the NPC performs a deadly finisher, putting an end to your journey.",
            "In a surprising turn of events, the NPC executes a lethal strike, ending your existence.",
            "With a swift motion, the NPC delivers a final blow, bringing your adventure to an end.",
            "You gasp in pain as the NPC unleashes a series of fatal strikes, sealing your fate.",
            "In a moment of despair, you fall to the NPC's finishing move, ending your story.",
            "With a powerful strike, the NPC delivers a finishing blow, ending your heroic quest.",
            "You feel the life drain from you as the NPC delivers a fatal strike, ending your journey.",
            "In a shocking twist, the NPC delivers a deadly finisher, putting an end to your legacy.",
            "With a swift motion, the NPC delivers a final blow, bringing your tale to a close.",
            "You watch helplessly as the NPC executes a lethal strike, ending your adventure.",
            "In a moment of defeat, you succumb to the NPC's finishing move, ending your tale.",
            "With a powerful blow, the NPC delivers a finishing strike, ending your heroic quest.",
            "You feel the end near as the NPC delivers a fatal blow, ending your journey.",
            "In a stunning turn of events, the NPC delivers a deadly finisher, ending your legacy.",
            "With a swift motion, the NPC delivers a final strike, bringing your story to an end.",
            "You watch in horror as the NPC executes a lethal finisher, ending your adventure.",
            "In a moment of despair, you fall to the NPC's finishing blow, sealing your fate.",
            "With a powerful strike, the NPC delivers a final blow, ending your heroic journey.",
            "You feel your strength wane as the NPC delivers a fatal strike, ending your tale."
        };

        private static string GetRandomComunicate(List<string> collection)
        {
            Random random = new Random();
            return collection[random.Next(0, collection.Count() - 1)];
        }

        public static string AttackParriedByPlayer() => GetRandomComunicate(attackParriedByPlayerDescription);
        public static string AttackParriedByNpc() => GetRandomComunicate(attackParriedByNpcDescription);
        public static string PlayerTakeDamage() => GetRandomComunicate(playerDamageDescriptions);
        public static string NpcTakeDamage() => GetRandomComunicate(npcDamageDescriptions);
        public static string AttackDodgedByPlayer() => GetRandomComunicate(playerDodgingDescriptions);
        public static string AttackDodgedByNpc() => GetRandomComunicate(npcEvadingDescriptions);
        public static string PerformFinisherByPlayer() => GetRandomComunicate(playerFinisherDescriptions);
        public static string PerformFinisherByNpc() => GetRandomComunicate(npcFinisherDescriptions);

        public static void GreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void RedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void YellowText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void BlueText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void DarkYellowText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }


    }
}
