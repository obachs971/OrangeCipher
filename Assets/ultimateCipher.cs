using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;

public class ultimateCipher : MonoBehaviour {
    
    public TextMesh[] screenTexts;
    public string[] wordList;
    public KMBombInfo Bomb;
    public KMBombModule module;
    public AudioClip[] sounds;
    public KMAudio Audio;
    public TextMesh submitText;
   
    
    private string[] matrixWordList =
      {
                "ACID",
                "BUST",
                "CODE",
                "DAZE",
                "ECHO",
                "FILM",
                "GOLF",
                "HUNT",
                "ITCH",
                "JURY",
                "KING",
                "LIME",
                "MONK",
                "NUMB",
                "ONLY",
                "PREY",
                "QUIT",
                "RAVE",
                "SIZE",
                "TOWN",
                "URGE",
                "VERY",
                "WAXY",
                "XYLO",
                "YARD",
                "ZERO",
                "ABORT",
                "BLEND",
                "CRYPT",
                "DWARF",
                "EQUIP",
                "FANCY",
                "GIZMO",
                "HELIX",
                "IMPLY",
                "JOWLS",
                "KNIFE",
                "LEMON",
                "MAJOR",
                "NIGHT",
                "OVERT",
                "POWER",
                "QUILT",
                "RUSTY",
                "STOMP",
                "TRASH",
                "UNTIL",
                "VIRUS",
                "WHISK",
                "XERIC",
                "YACHT",
                "ZEBRA",
                "ADVICE",
                "BUTLER",
                "CAVITY",
                "DIGEST",
                "ELBOWS",
                "FIXURE",
                "GOBLET",
                "HANDLE",
                "INDUCT",
                "JOKING",
                "KNEADS",
                "LENGTH",
                "MOVIES",
                "NIMBLE",
                "OBTAIN",
                "PERSON",
                "QUIVER",
                "RACHET",
                "SAILOR",
                "TRANCE",
                "UPHELD",
                "VANISH",
                "WALNUT",
                "XYLOSE",
                "YANKED",
                "ZODIAC",
                "ALREADY",
                "BROWSED",
                "CAPITOL",
                "DESTROY",
                "ERASING",
                "FLASHED",
                "GRIMACE",
                "HIDEOUT",
                "INFUSED",
                "JOYRIDE",
                "KETCHUP",
                "LOCKING",
                "MAILBOX",
                "NUMBERS",
                "OBSCURE",
                "PHANTOM",
                "QUIETLY",
                "REFUSAL",
                "SUBJECT",
                "TRAGEDY",
                "UNKEMPT",
                "VENISON",
                "WARSHIP",
                "XANTHIC",
                "YOUNGER",
                "ZEPHYRS",
                "ADVOCATE",
                "BACKFLIP",
                "CHIMNEYS",
                "DISTANCE",
                "EXPLOITS",
                "FOCALIZE",
                "GIFTWRAP",
                "HOVERING",
                "INVENTOR",
                "JEALOUSY",
                "KINSFOLK",
                "LOCKABLE",
                "MERCIFUL",
                "NOTECARD",
                "OVERCAST",
                "PERILOUS",
                "QUESTION",
                "RAINCOAT",
                "STEALING",
                "TREASURY",
                "UPDATING",
                "VERTICAL",
                "WISHBONE",
                "XENOLITH",
                "YEARLONG",
                "ZEALOTRY"
        };

    private string[][] pages;
    private string answer;
    private int page;
    private bool submitScreen;
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    public KMSelectable leftArrow;
    public KMSelectable rightArrow;
    public KMSelectable submit;
    public KMSelectable[] keyboard;
    void Awake()
    {
        moduleId = moduleIdCounter++;
        leftArrow.OnInteract += delegate () { left(leftArrow); return false; };
        rightArrow.OnInteract += delegate () { right(rightArrow); return false; };
        submit.OnInteract += delegate () { submitWord(submit); return false; };
        foreach(KMSelectable keybutton in keyboard)
        {
            KMSelectable pressedButton = keybutton;
            pressedButton.OnInteract += delegate () { letterPress(pressedButton); return false; };
        }
    }
        // Use this for initialization
        void Start ()
    
    {
        submitText.text = "1";
        //Generating random word
        answer = wordList[UnityEngine.Random.Range(0, wordList.Length)].ToUpper();
        Debug.LogFormat("[Orange Cipher #{0}] Generated Word: {1}", moduleId, answer);
       
        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = orangecipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string orangecipher(string word)
    {
        
        string encrypt = "";
        bool[] b = {false, false, false, false, false, false };
      
        for (int aa = 0; aa < 6; aa++)
        {
            if (word[aa] == 'J')
            {
                encrypt = encrypt + "ABCDEFGHIKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 26)];
                b[aa] = true;
            }
            else
            {
                encrypt = encrypt + "" + word[aa];
            }
        }
        string keyword1 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string keyword2 = "";
        bool flag = true;
        while(flag)
        {
            flag = false;
            keyword2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
            if (keyword2.IndexOf('J') >= 0 || keyword2.EqualsIgnoreCase(keyword1))
                flag = true;
        }
        string number = UnityEngine.Random.Range(0, 10000) + "";

        
        pages[1][1] = keyword1.ToUpper();
        pages[1][2] = number;
       
        string matrixa = keyword1.Replace('J', 'I');
        string matrixb = "AFLQVBGMRWCHNSXDIOTYEKPUZ";
        string matrixc;
        string matrixd = keyword2.Replace('J', 'I');
        int numberplace = 0;

        //Creating Matrix C by converting Number to Words.
        if (number.Equals("0"))
            matrixc = "ZERO";
        else
        {
            matrixc = "";
            for (int dd = number.Length - 1; dd >= 0; dd--)
            {
                switch (numberplace)
                {
                    case 0:
                        switch (number[dd])
                        {
                            case '1':
                                matrixc = "ONE" + matrixc;
                                break;
                            case '2':
                                matrixc = "TWO" + matrixc;
                                break;
                            case '3':
                                matrixc = "THREE" + matrixc;
                                break;
                            case '4':
                                matrixc = "FOUR" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIVE" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIX" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVEN" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHT" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINE" + matrixc;
                                break;
                        }
                        break;
                    case 1:
                        switch (number[dd])
                        {
                            case '1':
                                switch (number[dd + 1])
                                {
                                    case '1':
                                        matrixc = "ELEVEN";
                                        break;
                                    case '2':
                                        matrixc = "TWELVE";
                                        break;
                                    case '3':
                                        matrixc = "THIRTEEN";
                                        break;
                                    case '4':
                                        matrixc = "FOURTEEN";
                                        break;
                                    case '5':
                                        matrixc = "FIFTEEN";
                                        break;
                                    case '6':
                                        matrixc = "SIXTEEN";
                                        break;
                                    case '7':
                                        matrixc = "SEVENTEEN";
                                        break;
                                    case '8':
                                        matrixc = "EIGHTEEN";
                                        break;
                                    case '9':
                                        matrixc = "NINETEEN";
                                        break;
                                    case '0':
                                        matrixc = "TEN";
                                        break;
                                }
                                break;
                            case '2':
                                matrixc = "TWENTY" + matrixc;
                                break;
                            case '3':
                                matrixc = "THIRTY" + matrixc;
                                break;
                            case '4':
                                matrixc = "FORTY" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIFTY" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIXTY" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVENTY" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHTY" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINETY" + matrixc;
                                break;
                        }
                        break;
                    case 2:
                        switch (number[dd])
                        {
                            case '1':
                                matrixc = "ONEHUNDRED" + matrixc;
                                break;
                            case '2':
                                matrixc = "TWOHUNDRED" + matrixc;
                                break;
                            case '3':
                                matrixc = "THREEHUNDRED" + matrixc;
                                break;
                            case '4':
                                matrixc = "FOURHUNDRED" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIVEHUNDRED" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIXHUNDRED" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVENHUNDRED" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHTHUNDRED" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINEHUNDRED" + matrixc;
                                break;
                        }
                        break;
                    case 3:
                        switch (number[dd])
                        {
                            case '1':
                                matrixc = "ONETHOUSAND" + matrixc;
                                break;
                            case '2':
                                matrixc = "TWOTHOUSAND" + matrixc;
                                break;
                            case '3':
                                matrixc = "THREETHOUSAND" + matrixc;
                                break;
                            case '4':
                                matrixc = "FOURTHOUSAND" + matrixc;
                                break;
                            case '5':
                                matrixc = "FIVETHOUSAND" + matrixc;
                                break;
                            case '6':
                                matrixc = "SIXTHOUSAND" + matrixc;
                                break;
                            case '7':
                                matrixc = "SEVENTHOUSAND" + matrixc;
                                break;
                            case '8':
                                matrixc = "EIGHTTHOUSAND" + matrixc;
                                break;
                            case '9':
                                matrixc = "NINETHOUSAND" + matrixc;
                                break;
                        }
                        break;
                }
                numberplace++;
            }
        }
        matrixc = matrixc.Replace('J', 'I');
        string snnums = "";
        string sn = Bomb.GetSerialNumber();
        for (int ff = 0; ff < 6; ff++)
        {
            switch (sn[ff])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    snnums = snnums + "" + sn[ff];
                    break;
            }
        }
        matrixa = getKey(matrixa, "ABCDEFGHIKLMNOPQRSTUVWXYZ", Bomb.GetSerialNumberNumbers().Last() % 2 == 0);
        matrixc = getKey(matrixc, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[1] - '0') % 2 == 1);
        matrixd = getKey(matrixd, "ABCDEFGHIKLMNOPQRSTUVWXYZ", (snnums[0] - '0') % 2 == 0);

        
        
        Debug.LogFormat("[Orange Cipher #{0}] Matrix A: {1}", moduleId, matrixa);
        Debug.LogFormat("[Orange Cipher #{0}] Matrix B: {1}", moduleId, matrixb);
        Debug.LogFormat("[Orange Cipher #{0}] Matrix C: {1}", moduleId, matrixc);
        Debug.LogFormat("[Orange Cipher #{0}] Matrix D: {1}", moduleId, matrixd);
        Debug.LogFormat("[Orange Cipher #{0}] Begin Foursquare Encryption", moduleId);
        encrypt = FoursquareEnc(encrypt, matrixa, matrixb, matrixc, matrixd);

        Debug.LogFormat("[Orange Cipher #{0}] Begin Bazeries Encryption", moduleId);
        encrypt = BazeriesEnc(encrypt, matrixb, matrixc, number);

        Debug.LogFormat("[Orange Cipher #{0}] Begin ADFGX Encryption", moduleId);
        string kw2encrypt = ADFGXEnc(keyword2.ToUpper(), matrixa, keyword1.ToUpper());
        Debug.LogFormat("[Orange Cipher #{0}] Encrypted Keyword: {1}", moduleId, kw2encrypt);
        pages[1][0] = kw2encrypt.Substring(0, kw2encrypt.Length / 2);
        pages[0][2] = kw2encrypt.Substring(kw2encrypt.Length / 2);
        

        bool flag2 = true;
        for (int aa = 0; aa < 6; aa++)
        {
            if (b[aa])
            {
                pages[0][1] = pages[0][1] + "" + encrypt[aa];
                encrypt = encrypt.Substring(0, aa) + "J" + encrypt.Substring(aa + 1);
                flag2 = false;
            }
        }
        if(flag2)
        {
            pages[0][1] = "ABCDEFGHIKLMNOPQRSTUVWXYZ"[UnityEngine.Random.Range(0, 26)].ToString();
        }
        return encrypt;
    }
    
    string FoursquareEnc(string word, string ma, string mb, string mc, string md)
    {
        string encrypt = "";
        for (int gg = 0; gg < 6; gg++)
        {
            int n1 = ma.IndexOf(word[gg]);
            int n2 = md.IndexOf(word[gg + 1]);
            gg++;
            encrypt = encrypt + "" + mb[((n1 / 5) * 5) + (n2 % 5)] + "" + mc[(n1 % 5) + ((n2 / 5) * 5)];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, word[gg - 1] + "" + word[gg], encrypt[gg - 1] + "" + encrypt[gg]);
        }
        return encrypt;
    }
    string BazeriesEnc(string word, string mb, string mc, string num)
    {
        string encrypt = "";
        int n = 0;
        int subgroup = 0;
        for(int bb = 0; bb < num.Length; bb++)
            subgroup += (num[bb] - '0');
        subgroup = (subgroup % 4) + 2;
        for(int aa = 0; aa < word.Length; aa++)
        {
            char l = mb[mc.IndexOf(word[aa])];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, word[aa], l);
            encrypt = encrypt + "" + l;
            n++;
            if (n == subgroup)
            {
                encrypt = encrypt + "|";
                n = 0;
            }
        }
        string[] spl = encrypt.Split('|');
        string encrypt2 = "";
        for(int cc = 0; cc < spl.Length; cc++)
        {
         
            char[] temp = spl[cc].ToCharArray();
            Array.Reverse(temp);
            encrypt2 = encrypt2 + "" + new string(temp);
        }
        Debug.LogFormat("[Orange Cipher #{0}] Subgroup Number: ", subgroup);
        Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, encrypt, encrypt2);
        
        return encrypt2;
    }
    string ADFGXEnc(string word, string key, string kw)
    {
        string encrypt = "";
        string adfgx = "ADFGX";
        for(int aa = 0; aa < word.Length; aa++)
        {
            int num = key.IndexOf(word[aa]);
            encrypt = encrypt + "" + adfgx[num / 5] + "" + adfgx[num % 5];
            Debug.LogFormat("[Orange Cipher #{0}] {1} -> {2}", moduleId, word[aa], encrypt[aa * 2] + "" + encrypt[(aa * 2) + 1]);
        }
        int[] numrows = new int[kw.Length];
        
        for (int bb = 0; bb < kw.Length; bb++)
        {
            numrows[bb] = encrypt.Length / kw.Length;
            if (bb < (encrypt.Length % kw.Length))
                numrows[bb]++;
        }
        char[][] letters = new char[numrows[0]][];
        for(int cc = 0; cc < letters.Length; cc++)
        {
            letters[cc] = new char[kw.Length];
        }
        int cursor = 0;
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for(int dd = 0; dd < alpha.Length; dd++)
        {
            for(int ee = 0; ee < kw.Length; ee++)
            {
                if(kw[ee].ToString().IndexOf(alpha[dd]) >= 0)
                {
                    string templog = "";
                    for(int ff = 0; ff < numrows[ee]; ff++)
                    {
                        letters[ff][ee] = encrypt[cursor];
                        templog = templog + "" + encrypt[cursor];
                        cursor++;
                    }
                    Debug.LogFormat("[Orange Cipher #{0}] {1}: {2}", moduleId, kw[ee], templog);
                }
            }
            if (cursor >= encrypt.Length)
                break;
        }
        string logoutput = kw.ToUpper() + "\n";
        int row = 0;
        int col = 0;
        encrypt = "";
        for (int gg = 0; gg < cursor; gg++)
        {
            encrypt = encrypt + "" + letters[row][col];
            logoutput = logoutput + "" + letters[row][col];
            col++;
            if(col >= letters[row].Length)
            {
                col = 0;
                row++;
                logoutput = logoutput + "\n";
            }
        }
        Debug.LogFormat("[Orange Cipher #{0}] {1}", moduleId, logoutput);
        return encrypt;
    }
    string getKey(String k, String alpha, bool start)
    {
        for (int aa = 0; aa < k.Length; aa++)
        {
            for (int bb = aa + 1; bb < k.Length; bb++)
            {
                if (k[aa] == k[bb])
                {
                    k = k.Substring(0, bb) + "" + k.Substring(bb + 1);
                    bb--;
                }
            }
            alpha = alpha.Replace(k[aa].ToString(), "");
        }
        if (start)
            return (k + "" + alpha);
        else
            return (alpha + "" + k);
    }
	int correction(int p, int max)
    {
        while (p < 0)
            p += max;
        while (p >= max)
            p -= max;
        return p;
    }
    void left(KMSelectable arrow)
    {
        if(!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page--;
            page = correction(page, pages.Length);
            getScreens();
        }
    }
    void right(KMSelectable arrow)
    {
        if(!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page++;
            page = correction(page, pages.Length);
            getScreens();
        }
    }
    private void getScreens()
    {
        submitText.text = (page + 1) + "";
        screenTexts[0].text = pages[page][0];
        screenTexts[1].text = pages[page][1];
        screenTexts[2].text = pages[page][2];
        screenTexts[0].fontSize = 40;
        screenTexts[1].fontSize = 40;
        screenTexts[2].fontSize = 40;
        
    }
    void submitWord(KMSelectable submitButton)
    {
        if(!moduleSolved)
        {
            submitButton.AddInteractionPunch();
            if(screenTexts[2].text.Equals(answer))
            {
                Audio.PlaySoundAtTransform(sounds[2].name, transform);
                module.HandlePass();
                moduleSolved = true;
                screenTexts[2].text = "";
            }
            else
            {
                Audio.PlaySoundAtTransform(sounds[3].name, transform);
                module.HandleStrike();
                page = 0;
                getScreens();
                submitScreen = false;
            }
        }
    }
    void letterPress(KMSelectable pressed)
    {
        if(!moduleSolved)
        {
            pressed.AddInteractionPunch();
            Audio.PlaySoundAtTransform(sounds[1].name, transform);
            if (submitScreen)
            {
                if(screenTexts[2].text.Length < 6)
                {
                    screenTexts[2].text = screenTexts[2].text + "" + pressed.GetComponentInChildren<TextMesh>().text;
                }
            }
            else
            {
                submitText.text = "SUB";
                screenTexts[0].text = "";
                screenTexts[1].text = "";
                screenTexts[2].text = pressed.GetComponentInChildren<TextMesh>().text;
                submitScreen = true;
            }
        }
    }
#pragma warning disable 414
    private string TwitchHelpMessage = "Move to other screens using !{0} right|left|r|l|. Submit the decrypted word with !{0} submit qwertyuiopasdfghjklzxcvbnm";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        
        if(command.EqualsIgnoreCase("right") || command.EqualsIgnoreCase("r"))
        {
            right(rightArrow);
            yield return new WaitForSeconds(0.1f);
            yield return null;
        }
        if (command.EqualsIgnoreCase("left") || command.EqualsIgnoreCase("l"))
        {
            left(leftArrow);
            yield return new WaitForSeconds(0.1f);
            yield return null;
        }
        string[] split = command.ToUpperInvariant().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length != 2 || !split[0].Equals("SUBMIT") || split[1].Length != 6) yield break;
        int[] buttons = split[1].Select(getPositionFromChar).ToArray();
        if (buttons.Any(x => x < 0)) yield break;
        yield return null;

        yield return new WaitForSeconds(0.1f);
        foreach (char let in split[1])
        {
            letterPress(keyboard[getPositionFromChar(let)]);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        submit.OnInteract();
        yield return new WaitForSeconds(0.1f);
    }

    private int getPositionFromChar(char c)
    {
        return "QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(c);
    }
}
