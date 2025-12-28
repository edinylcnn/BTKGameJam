using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

public class actionManager : MonoBehaviour
{
    public Button firstUpgrade;
    public Button secondUpgrade;
    public Button thirdUpgrade;
    public Text firstUpgradeText;
    public Text secondUpgradeText;
    public Text thirdUpgradeText;
    private Texture firstUpgradeTexture;
    private Texture secondUpgradeTexture;
    private Texture thirdUpgradeTexture;
    private RawImage rawImage1;
    private RawImage rawImage2;
    private RawImage rawImage3;
    List<int> upgrades = new List<int>() { 1, 2, 3, 4, 5 }; //1:health 2:attackdmg 3:defense 4:blockshield 5:pandorabox
    List<int> selected;
    public YusufScripts.Player player;

    public bool IsSelectionActive = false;
    public Texture[] textures;
    public Texture[] texturesHell;

    void Start()
    {
        firstUpgrade.onClick.AddListener(Choosen1);
        secondUpgrade.onClick.AddListener(Choosen2);
        thirdUpgrade.onClick.AddListener(Choosen3);
        rawImage1= firstUpgrade.GetComponent<RawImage>();
        rawImage2 = secondUpgrade.GetComponent<RawImage>();
        rawImage3 = thirdUpgrade.GetComponent<RawImage>();
        
    }

    public void OnWallTriggered()
    {
        IsSelectionActive = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        selected = upgrades.OrderBy(x => Random.value).Take(3).ToList();

        if (selected[0]==1)
        {
            firstUpgradeText.text = "Health + 5";
            
            rawImage1.texture = textures[0];
        }else if(selected[0]==2)
        {
            firstUpgradeText.text = "Attack + 1";
            
            rawImage1.texture = textures[1];
        }
        else if (selected[0]==3)
        {
            firstUpgradeText.text = "Defense + 0.1";
            
            rawImage1.texture = textures[2];
        }
        else if (selected[0]==4)
        {
            firstUpgradeText.text = "Block Shield";
            
            rawImage1.texture = textures[3];
        }
        else if (selected[0]==5)
        {
            firstUpgradeText.text = "Pandora Box";
            
            rawImage1.texture = textures[4];
        }



        if (selected[1] == 1)
        {
            secondUpgradeText.text = "Health + 5";
            
            rawImage2.texture = textures[0];
        }
        else if (selected[1] == 2)
        {
            secondUpgradeText.text = "Attack + 1";
            
            rawImage2.texture = textures[1];
        }
        else if (selected[1] == 3)
        {
            secondUpgradeText.text = "Defense + 0.1";
            
            rawImage2.texture = textures[2];
        }
        else if (selected[1] == 4)
        {
            secondUpgradeText.text = "Block Shield";
            
            rawImage2.texture = textures[3];
        }
        else if (selected[1] == 5)
        {
            secondUpgradeText.text = "Pandora Box";
            
            rawImage2.texture = textures[4];
        }



        if (selected[2] == 1)
        {
            thirdUpgradeText.text = "Health + 5";
            
            rawImage3.texture = textures[0];
        }
        else if (selected[2] == 2)
        {
            thirdUpgradeText.text = "Attack + 1";
            
            rawImage3.texture = textures[1];
        }
        else if (selected[2] == 3)
        {
            thirdUpgradeText.text = "Defense + 0.1";
            
            rawImage3.texture = textures[2];
        }
        else if (selected[2] == 4)
        {
            thirdUpgradeText.text = "Block Shield";
            
            rawImage3.texture = textures[3];
        }
        else if (selected[2] == 5)
        {
            thirdUpgradeText.text = "Pandora Box";
            
            rawImage3.texture = textures[4];
        }

        firstUpgrade.gameObject.SetActive(true);
        secondUpgrade.gameObject.SetActive(true);
        thirdUpgrade.gameObject.SetActive(true);
    }

    void Choosen1()
    {
        if (selected[0]==1)
        {
            player.health += 5f; 
        }else if(selected[0]==2)
        {
            player.attackdmg += 1f;
        }else if( selected[0]==3)
        {
            player.defense += 0.1f;
        }else if (selected[0]==4)
        {
            player.hasBlockShield=true;
        }else if (selected[0]==5)
        {
            int rand = Random.Range(1, 4);//1:can 2:atackdmg 3:defense
            int rand2 = Random.Range(1,6);//1,2,3,4: - de�er    5: + de�er
            if(rand==1)
            {
                if(rand2==5)
                {
                    player.health += 15f;
                }else
                {
                    player.health -= 3f;
                }
            }else if(rand==2)
            {
                if( rand2==5)
                {
                    player.attackdmg += 4f;
                }else
                {
                    player.attackdmg -= 0.4f;
                }
            }else if(rand==3)
            {
                if(rand2==5)
                {
                    player.defense += 0.4f;
                }else
                {
                    player.defense -= 0.1f;
                }
            }
        }

        firstUpgrade.gameObject.SetActive(false);
        secondUpgrade.gameObject.SetActive(false);
        thirdUpgrade.gameObject.SetActive(false);
        IsSelectionActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Choosen2()
    {
        if (selected[1] == 1)
        {
            player.health += 5f;
        }
        else if (selected[1] == 2)
        {
            player.attackdmg += 1f;
        }
        else if (selected[1] == 3)
        {
            player.defense += 0.1f;
        }
        else if (selected[1] == 4)
        {
            player.hasBlockShield = true;
        }
        else if (selected[1] == 5)
        {
            int rand = Random.Range(1, 4);//1:can 2:atackdmg 3:defense
            int rand2 = Random.Range(1, 6);//1,2,3,4: - de�er    5: + de�er
            if (rand == 1)
            {
                if (rand2 == 5)
                {
                    player.health += 15f;
                }
                else
                {
                    player.health -= 3f;
                }
            }
            else if (rand == 2)
            {
                if (rand2 == 5)
                {
                    player.attackdmg += 4f;
                }
                else
                {
                    player.attackdmg -= 0.4f;
                }
            }
            else if (rand == 3)
            {
                if (rand2 == 5)
                {
                    player.defense += 0.4f;
                }
                else
                {
                    player.defense -= 0.1f;
                }
            }
        }

        firstUpgrade.gameObject.SetActive(false);
        secondUpgrade.gameObject.SetActive(false);
        thirdUpgrade.gameObject.SetActive(false);
        IsSelectionActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Choosen3()
    {
        if (selected[2] == 1)
        {
            player.health += 5f;
        }
        else if (selected[2] == 2)
        {
            player.attackdmg += 1f;
        }
        else if (selected[2] == 3)
        {
            player.defense += 0.1f;
        }
        else if (selected[2] == 4)
        {
            player.hasBlockShield = true;
        }
        else if (selected[2] == 5)
        {
            int rand = Random.Range(1, 4);//1:can 2:atackdmg 3:defense
            int rand2 = Random.Range(1, 6);//1,2,3,4: - de�er    5: + de�er
            if (rand == 1)
            {
                if (rand2 == 5)
                {
                    player.health += 15f;
                }
                else
                {
                    player.health -= 3f;
                }
            }
            else if (rand == 2)
            {
                if (rand2 == 5)
                {
                    player.attackdmg += 4f;
                }
                else
                {
                    player.attackdmg -= 0.4f;
                }
            }
            else if (rand == 3)
            {
                if (rand2 == 5)
                {
                    player.defense += 0.4f;
                }
                else
                {
                    player.defense -= 0.1f;
                }
            }
        }

        firstUpgrade.gameObject.SetActive(false);
        secondUpgrade.gameObject.SetActive(false);
        thirdUpgrade.gameObject.SetActive(false);
        IsSelectionActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
