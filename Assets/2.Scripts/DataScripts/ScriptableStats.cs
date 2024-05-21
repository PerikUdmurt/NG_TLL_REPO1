using UnityEngine;

    [CreateAssetMenu(menuName = "ScriptableObjects/ScriptableStats")]
    public class ScriptableStats : ScriptableObject
    {
        [Header("LAYERS")]
        
        [Tooltip("Установить на слой, на котором находится игрок\n\nSet this to the layer your player is on")]
        public LayerMask PlayerLayer;

        
        [Header("INPUT")]
        
        [Tooltip("Превращение всех входных данных в целое число. Предотвращает замедление геймпадов. Рекомендуемое значение true обеспечивает равенство геймпада и клавиатуры\n\nMakes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity")]
        public bool SnapInput = true;
        
        [Tooltip("Использование лестниц и подъемов требует минимум усилий. Избегает нежелательного подъема с помощью контроллеров\n\nMinimum input required before you mount a ladder or climb a ledge. Avoids unwanted climbing using controllers"), Range(0.01f, 0.99f)]
        public float VerticalDeadZoneThreshold = 0.3f;
        
        [Tooltip("Быстрое распознавание левоего и правого направления. Избегает отклонения с залипшими контроллерами\n\nMinimum input required before a left or right is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
        public float HorizontalDeadZoneThreshold = 0.1f;
        
        
        [Header("MOVEMENT")]
        
        [Tooltip("Максимальная скорость горизонтального перемещения\n\nThe top horizontal movement speed")]
        public float MaxSpeed = 14;
        
        [Tooltip("Способность игрока набирать горизонтальную скорость\n\nThe player's capacity to gain horizontal speed")]
        public float Acceleration = 120;
        
        [Tooltip("Скорость, с которой игрок останавливается\n\nThe pace at which the player comes to a stop")]
        public float GroundDeceleration = 60;
        
        [Tooltip("Замедление в воздухе после прекращения нажатия.\n\nDeceleration in air only after stopping input mid-air")]
        public float AirDeceleration = 30;
        
        [Tooltip("Постоянная направленная вниз сила, действующая при приземлении. Помогает на склонах\n\nA constant downward force applied while grounded. Helps on slopes"), Range(0f, -10f)]
        public float GroundingForce = -1.5f;
        
        [Tooltip("Расстояние обнаружения пола и потолка\n\nThe detection distance for grounding and roof detection"), Range(0f, 0.5f)]
        public float GrounderDistance = 0.05f;

        [Tooltip("Сила рывка")]
        public float DashPower = 50f;

        [Tooltip("Ускорение рывка")]
        public float DashAcceleration = 50f;


    [Header("JUMP")]

        [Tooltip("Максимальное количество прыжков")]
        public int MaxAmountJump = 1;

        [Tooltip("Непосредственная скорость, применяемая при прыжке\n\nThe immediate velocity applied when jumping")]
        public float JumpPower = 36;

        [Tooltip("Максимальная скорость вертикального перемещения\n\nThe maximum vertical movement speed")]
        public float MaxFallSpeed = 40;
        
        [Tooltip("Способность игрока увеличивать скорость падения\n\nThe player's capacity to gain fall speed")]
        public float FallAcceleration = 110;

        [Tooltip("Способность игрока увеличивать скорость скольжения по стене\n\nThe player's capacity to gain fall speed")]
        public float wallFallAcceleration = 20;

        [Tooltip("Множитель гравитации при раннем отпускании клавиши прыжка\n\n" +
                 "The gravity multiplier added when jump is released early")]
        public float JumpEndEarlyGravityModifier = 3;

        [Tooltip("Время становления непригодности прыжка койота. Позволяет выполнять прыжок даже после ухода с платформы\n\nThe time before coyote jump becomes unusable. Coyote jump allows jump to execute even after leaving a ledge")]
        public float CoyoteTime = .15f;

        [Tooltip("Буфферизация прыжка. Позволяет активировать прыжок до приземления\n\nThe amount of time we buffer a jump. This allows jump input before actually hitting the ground")]
        public float JumpBuffer = .2f;

        [Tooltip("Горизонтальная скорость при прыжке от стены\n\nThe amount of time we buffer a jump. This allows jump input before actually hitting the ground")]
        public float HorizontalJumpAfterHanging = .2f;

    [Header("ATTACK")]

        [Tooltip("Сила атаки")]
        public float Damage = 10f;

        [Tooltip("Время, перед следующей атакой")]
        public float CooldownTime = 10f;

        [Tooltip("Задержка перед атакой")]
        public float DelayBeforeTheAttack = 10f;

        [Header("HEALTH")]

        [Tooltip("Общее здоровье")]
        public float Health = 100f;

        [Tooltip("Делит получаемый урон на данное число")]
        public float Armour = 1f;

        //Добавить сопротивление разным типам урона (Bool), если это предполагается.
    } 