using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>

public class HitCoin : MonoBehaviour
{
	public AudioClip CoinSound = null;

	private AudioSource mAudioSource = null;

	private GameObject operacao;
	private GameObject canvas;
	private GameObject operacao1;
	private GameObject operacao2;
	private GameObject operacao3;
	private GameObject operacao4;
	private GameObject player;
	private GameObject pontos;
	private GameObject fase;
	private GameObject[] qntCoin;
	private int tentativas = 3;

	private OperationGenerator op = new OperationGenerator();
	private SceneLoadCounter sc = new SceneLoadCounter();

	//private List<String> operacao;

	void Start()
	{
		mAudioSource = GetComponent<AudioSource>();
		operacao = GameObject.FindWithTag("Operacao");
		canvas = GameObject.FindWithTag("Canvas");
		operacao1 = GameObject.FindWithTag("Operacao1");
		operacao2 = GameObject.FindWithTag("Operacao2");
		operacao3 = GameObject.FindWithTag("Operacao3");
	 	operacao4 = GameObject.FindWithTag("Operacao4");
		player = GameObject.FindWithTag("Player");
		pontos = GameObject.FindWithTag("Pontos");
		fase = GameObject.FindWithTag("Fase");

		fase.GetComponent<TextMesh>().text = (sc.getCount() + 1).ToString();
	}

	void Update()
    {
		qntCoin = GameObject.FindGameObjectsWithTag("Coin");
		if(qntCoin.Length == 0)
        {
			Debug.Log("YOU WIN");
			Scene scene = SceneManager.GetActiveScene();
			sc.IncrementSceneLoad(scene, LoadSceneMode.Additive);
			SceneManager.LoadScene(scene.name);
		}
		if(tentativas == 0)
        {
			Debug.Log("YOU LOOSE");
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}
	}

    public void ClickMenu(Button btn)
    {
		if (btn.GetComponentInChildren<TextMeshProUGUI>().text == op.getResposta())
        {
			btn.image.color = Color.green;

			pontos.GetComponent<TextMesh>().text = (int.Parse(pontos.GetComponent<TextMesh>().text) + 1).ToString();
			player.GetComponent<SimpleSampleCharacterControl>().enabled = true;

			operacao.GetComponent<TextMesh>().characterSize = 0;
			canvas.GetComponent<Canvas>().scaleFactor = 0;
		}
        else
        {
			btn.image.color = Color.red;
			tentativas--;
		}
	}

    void OnTriggerEnter(Collider other)
	{
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

			operacao.GetComponent<TextMesh>().text = op.getExpressao();
			operacao.GetComponent<TextMesh>().characterSize = 0.5f;

			canvas.GetComponent<Canvas>().scaleFactor = 1;

			operacao1.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();
			operacao2.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();
			operacao3.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();
			operacao4.GetComponentInChildren<TextMeshProUGUI>().text = op.getOpcao();

			player.GetComponent<SimpleSampleCharacterControl>().enabled = false;
		}
	}
}
