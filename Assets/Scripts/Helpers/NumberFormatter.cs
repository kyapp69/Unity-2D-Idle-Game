[System.Serializable]
public class NumberFormatter {

    // todo: a>...>z>A>...>Z>aa>ab>..>zy>zz>AA>...>ZZ
    private static readonly string[] postfixes = new string[]
        {"", "kil", "mil", "bil", "tril", "a", "b", "c", "d", "aa", "ab"};

    public static string format(float num, int monospace = 0) {
        float showNum = num;
        int maxPostfixLength = postfixes.Length;
        int i = 0;
        for (; i < maxPostfixLength; i++) {
            if (showNum >= 1000) {
                showNum = showNum / 1000;
            } else {
                break;
            }
        }

        if (monospace > 0) {
            return monospaced(showNum.ToString("F1"), monospace) + postfixes[i];
        } else {
            return showNum.ToString("F1") + postfixes[i];
        }
    }

    private static string monospaced(string text, int size) {
        return "<mspace=" + size + ">" + text + "</mspace>";
    }
}