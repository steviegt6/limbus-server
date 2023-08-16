using Newtonsoft.Json;

namespace Tomat.LimbusServer.Api.Models;

// public enum ElementType {
//     /*NONE*/
//     None = -1,
// 
//     /*ITEM,*/
//     Item,
// 
//     /*EXP,*/
//     Exp,
// 
//     /*CHARACTER,*/
//     Character,
// 
//     /*PERSONALITY,*/
//     Personality,
// 
//     /*EGO,*/
//     Ego,
// 
//     /*STAMINA,*/
//     Stamina,
// 
//     /*BATTLEPASS_POINT,*/
//     BattlePassPoint,
// 
//     /*VENDING_MACHINE,*/
//     VendingMachine,
// 
//     /*ANNOUNCER,*/
//     Announcer,
// 
//     /*EGO_GIFT,*/
//     EgoGift,
// 
//     /*GACHA,*/
//     Gacha,
// 
//     /*USERBANNER,*/
//     UserBanner,
// 
//     /*VENDING_MACHINE_PERSONALITY,*/
//     VendingMachinePersonality,
// 
//     /*VENDING_MACHINE_CHARACTER,*/
//     VendingMachineCharacter,
// 
//     /*PERSONALITYEXP,*/
//     PersonalityExp,
// 
//     /*PERSONALITYLEVEL,*/
//     PersonalityLevel,
// 
//     /*GACKSUNGPIECE,*/
//     GacksungPiece,
// 
//     /*STAMINAMAX,*/
//     StaminaMax,
// 
//     /*SEASONAL_R_BOX,*/
//     SeasonalRBox,
// 
//     /*SEASONAL_O_BOX,*/
//     SeasonalOBox,
// 
//     /*SEASONAL_PIECE,*/
//     SeasonalPiece,
// 
//     /*EVENT_ITEM,*/
//     EventItem,
// 
//     /*USER_TICKET_DECO_LEFT,*/
//     UserTicketDecoLeft,
// 
//     /*USER_TICKET_DECO_RIGHT,*/
//     UserTicketDecoRight,
// 
//     /*USER_TICKET_DECO_EGOBG,*/
//     UserTicketDecoEgobg,
// 
//     /*USER_TICKET_DECO_FOR_UI,*/
//     UserTicketDecoForUI,
// 
//     /*MIRRORDUNGEON_COST,*/
//     MirrorDungeonCost,
// 
//     /*UNLOCK_CODE,*/
//     UnlockCode,
// 
//     /*CHANCE*/
//     Chance,
// }

public class Element {
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("num")]
    public int Num { get; set; }
}
