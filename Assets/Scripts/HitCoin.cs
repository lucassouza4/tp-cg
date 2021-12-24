using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>

public class HitCoin : MonoBehaviour
{
	public AudioClip CoinSound = null;

	private AudioSource mAudioSource = null;

	public bool resposta = false;
	public TMP_Text win;
	public TMP_Text lose;

	private GameObject operacao;
	private GameObject panel;
	private GameObject operacao1;
	private GameObject operacao2;
	private GameObject operacao3;
	private GameObject operacao4;
	private GameObject player;
	private GameObject pontos;
	private GameObject fase;
	private GameObject time;
	private GameObject[] qntCoin;
	private int tentativas = 3;

	private OperationGenerator op = new OperationGenerator();
	private SceneLoadCounter sc = new SceneLoadCounter();

	//private List<String> operacao;

	void Start()
	{
		mAudioSource = GetComponent<AudioSource>();
		operacao = GameObject.FindWithTag("Operacao");
		panel = GameObject.FindWithTag("Panel");
		operacao1 = GameObject.FindWithTag("Operacao1");
		operacao2 = GameObject.FindWithTag("Operacao2");
		operacao3 = GameObject.FindWithTag("Operacao3");
	 	operacao4 = GameObject.FindWithTag("Operacao4");
		player = GameObject.FindWithTag("Player");
		pontos = GameObject.FindWithTag("Pontos");
		fase = GameObject.FindWithTag("Fase");
		time = GameObject.FindWithTag("Timer");

		fase.GetComponent<TextMeshProUGUI>().text = (sc.getCount() + 1).ToString();
	}

	void Update()
    {
		qntCoin = GameObject.FindGameObjectsWithTag("Coin");
		if( resposta == true)
        {
			win.rectTransform.localScale = new Vector3(1, 1, 1);
			//restart(5);
			Scene scene = SceneManager.GetActiveScene();
			sc.IncrementSceneLoad(scene, LoadSceneMode.Additive);
			SceneManager.LoadScene(scene.name);
		}
		if(tentativas == 0 || time.GetComponent<TextMeshProUGUI>().text == "0")
        {
			lose.rectTransform.localScale = new Vector3(1, 1, 1);
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}
	}

	public void restart(float t)
    {
		while(t != 0)
        {
			t -= 1 * Time.deltaTime;
		}
    }
    public void ClickMenu(Button btn)
    {
		if (btn.GetComponentInChildren<TextMeshProUGUI>().text == op.getResposta())
        {
			btn.image.color = Color.green;

			pontos.GetComponent<TextMeshProUGUI>().text = (int.Parse(pontos.GetComponent<TextMeshProUGUI>().text) + 1).ToString();
			player.GetComponent<SimpleSampleCharacterControl>().enabled = true;

			operacao.GetComponent<TextMeshProUGUI>().fontSize = 0;
			panel.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
			resposta = true;
		}
        else
        {
			btn.image.color = Color.red;
			tentativas--;
		}
	}

    void OnTriggerEnter(Collider other)
	{
		resposta = false;
		tentativas = 3;
		operacao1.GetComponent<Button>().image.color = Color.white;
		operacao2.GetComponent<Button>().image.color = Color.white;
		operacao3.GetComponent<Button>().image.color = Color.white;
		operacao4.GetComponent<Button>().image.color = Color.white;

		if (other.gameObject.tag.Equals("Coin"))
		{
			if (mAudioSource != null && CoinSound != null)
			{
				mAudioSource.PlayOneShot(CoinSound);
			}

			Destroy(other.gameObject);

			op.GerarOperacoes();

			operacao.GetComponent<TextMeshProUGUI>().text = op.getExpressao();
			operacao.GetComponent<TextMeshProUGUI>().fontSize = 36;

			panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

			operacao1.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();
			operacao2.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();
			operacao3.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();
			operacao4.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();

			player.GetComponent<SimpleSampleCharacterControl>().enabled = false;
		}
	}
}
