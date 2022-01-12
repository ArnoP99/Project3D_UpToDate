using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationElementInitializer
{
    private static ConversationElement startElement1 = new ConversationElement("Goedemiddag, alles in orde?", ConversationElement.ElementState.Neutral, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_GoedeMiddag") , ConversationElement.ActiveState.Continue);
    private static ConversationElement agressief1A = new ConversationElement("Neen, ik wil hier weg!", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_NeeIkWilWeg") , ConversationElement.ActiveState.Continue);
    private static ConversationElement neutraal1A = new ConversationElement("Wanneer mag ik terug naar huis?", ConversationElement.ElementState.Neutral, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_WanneerMagIkNaarHuis") , ConversationElement.ActiveState.Continue);
    private static ConversationElement defensief1A = new ConversationElement("Weet u al wanneer ik terug naar huis mag?", ConversationElement.ElementState.Defensive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_WeetUAlWanneerIkTerugNaarHuisMag") , ConversationElement.ActiveState.Continue);

    private static ConversationElement agressief1B = new ConversationElement("Niemand houdt u tegen!", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_NietmandHoudtUTegen") , ConversationElement.ActiveState.Ended);
    private static ConversationElement neutraal1B = new ConversationElement("Waarom wilt u weg?", ConversationElement.ElementState.Neutral, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_WaaromWiltUWeg") , ConversationElement.ActiveState.Continue);
    private static ConversationElement defensief1B = new ConversationElement("De dokter heeft gezegd dat je nog een paar dagen moet blijven.", ConversationElement.ElementState.Defensive, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_NogEenPaarDagenBlijven") , ConversationElement.ActiveState.Continue);

    private static ConversationElement agressief1C1 = new ConversationElement("Nee ik wil hier nu weg!", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_NeeIkWilNUWeg"), ConversationElement.ActiveState.Phase2);
    private static ConversationElement defensief1C1 = new ConversationElement("Oke, een paar dagen moet nog wel lukken.", ConversationElement.ElementState.Defensive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_OkePaarDagenMoetNogLukken") , ConversationElement.ActiveState.Ended);

    private static ConversationElement agressief1C2 = new ConversationElement("Ik ben het beu zit hier nu 2 weken en heb nog geen verbetering gezien kan het beter zelf doen!", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_IkBenHetBeuIkKanHetZelfBeter") , ConversationElement.ActiveState.Continue);
    private static ConversationElement neutraal1C2 = new ConversationElement("ik voel me in orde ik ben genezen waarom ben ik hier nog?", ConversationElement.ElementState.Neutral, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_IkVoelMeGenezen") , ConversationElement.ActiveState.Continue);

    private static ConversationElement agressief1D1 = new ConversationElement("Als u denkt dat u het zelf beter kan doe maar.", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_DenktDatUHetBeterKan") , ConversationElement.ActiveState.Ended);
    private static ConversationElement neutraal1D1 = new ConversationElement("Sorry meneer ik kan u niet laten gaan.", ConversationElement.ElementState.Defensive, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_NietLatenGaan") , ConversationElement.ActiveState.Phase2);

    private static ConversationElement agressief1D2 = new ConversationElement("Omdat ik het zeg!", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_OmdatIkHetZeg") , ConversationElement.ActiveState.Phase2);
    private static ConversationElement neutraal1D2 = new ConversationElement("U bent toch al wat beter een paar dagen extra ter controle zal nog wel lukken, toch?", ConversationElement.ElementState.Defensive, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Nurse/NurseVC_NogEenPaarDagenBlijven") , ConversationElement.ActiveState.Continue);

    private static ConversationElement defensief1E = new ConversationElement("Oke, u hebt gelijk. Ik zal nog blijven.", ConversationElement.ElementState.Defensive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_OkePaarDagenMoetNogLukken"), ConversationElement.ActiveState.Ended);
    private static ConversationElement agressief1E = new ConversationElement("Ik hoef geen controle meer, laat me gewoon vertrekken!", ConversationElement.ElementState.Agressive, ConversationElement.UserState.Agressor, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_HoefGeenControleMeer") , ConversationElement.ActiveState.Phase2);

    private static ConversationElement startElement2 = new ConversationElement("Goedemidag, het is tijd voor uw medicatie.", ConversationElement.ElementState.Neutral, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_WeetUAlWanneerIkTerugNaarHuisMag") , ConversationElement.ActiveState.Continue);
    private static ConversationElement startElement3 = new ConversationElement("Is er een probleem? Ik zag dat u op het knopje voor hulp heeft geduwd.", ConversationElement.ElementState.Neutral, ConversationElement.UserState.Nurse, Resources.Load<AudioClip>("AudioFiles/VoiceLines_Agressor/AgressorVC_WeetUAlWanneerIkTerugNaarHuisMag") , ConversationElement.ActiveState.Continue);

    public static void SetReactionElements()
    {
        startElement1.AddElementToReactions(agressief1A);
        startElement1.AddElementToReactions(neutraal1A);
        startElement1.AddElementToReactions(defensief1A);

        startElement2.AddElementToReactions(agressief1A);
        startElement2.AddElementToReactions(neutraal1A);
        startElement2.AddElementToReactions(defensief1A);

        startElement3.AddElementToReactions(agressief1A);
        startElement3.AddElementToReactions(neutraal1A);
        startElement3.AddElementToReactions(defensief1A);

        agressief1A.AddElementToReactions(agressief1B);
        agressief1A.AddElementToReactions(neutraal1B);
        agressief1A.AddElementToReactions(defensief1B);

        neutraal1A.AddElementToReactions(agressief1B);
        neutraal1A.AddElementToReactions(neutraal1B);
        neutraal1A.AddElementToReactions(defensief1B);

        defensief1A.AddElementToReactions(agressief1B);
        defensief1A.AddElementToReactions(neutraal1B);
        defensief1A.AddElementToReactions(defensief1B);

        //agressief1B --> einde conversatie

        neutraal1B.AddElementToReactions(agressief1C2);
        neutraal1B.AddElementToReactions(neutraal1C2);

        defensief1B.AddElementToReactions(agressief1C1);
        defensief1B.AddElementToReactions(defensief1C1);

        //agressief1C1 --> Over naar fase 2
        //defensief1C1 --> Einde conversatie

        agressief1C2.AddElementToReactions(neutraal1D1);
        agressief1C2.AddElementToReactions(agressief1D1);

        neutraal1C2.AddElementToReactions(agressief1D2);
        neutraal1C2.AddElementToReactions(neutraal1D2);

        //neutraal1D1 --> Over naar fase 2
        //agressief1D1 --> Einde conversatie

        //agressief1D2 --> Over naar fase 2

        neutraal1D2.AddElementToReactions(defensief1E);
        neutraal1D2.AddElementToReactions(agressief1E);

        //defensief1E --> Einde conversatie
        //agressief1E --> Over naar fase 2
    }

    // Method used to send staring element to ConversationManager to initialize Conversation: GeneralCheckup
    public static ConversationElement GeneralCheckupConversation()
    {
        return startElement1;
    }

    public static ConversationElement TimeForMedicationConversation()
    {
        return startElement2;
    }

    public static ConversationElement HelpButtonConversation()
    {
        return startElement3;
    }
}
