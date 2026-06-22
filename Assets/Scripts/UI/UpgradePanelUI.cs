using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelUI : MonoBehaviour
{
    [Header("Card Pools")]
    [SerializeField] private List<UpgradeSO> allUpgradesPool = new List<UpgradeSO>(); // Перетащи сюда все свои ScriptableObjects карточек

    [Header("UI References")]
    [SerializeField] private Transform cardsContainer;      // Объект (например, с Horizontal Layout Group), где лежат карточки
    [SerializeField] private GameObject cardPrefab;        // Префаб UI-карточки (с компонентом UpgradeCardUI)
    [SerializeField] private int cardsToGenerate = 3;      // Сколько карточек показывать на выбор (обычно 3)

    private List<GameObject> spawnedCards = new List<GameObject>();

    public void GenerateCards()
    {
        // Очищаем старые карточки перед генерацией новых
        ClearOldCards();

        if (allUpgradesPool.Count == 0)
        {
            Debug.LogError("Пул карточек улучшений пуст!");
            return;
        }

        // Создаем временную копию пула, чтобы избежать дубликатов на одном экране выбора
        List<UpgradeSO> availableCards = new List<UpgradeSO>(allUpgradesPool);

        // Выбираем случайные карточки
        for (int i = 0; i < cardsToGenerate; i++)
        {
            if (availableCards.Count == 0) break; // Защита, если пул карточек меньше, чем cardsToGenerate

            int randomIndex = Random.Range(0, availableCards.Count);
            UpgradeSO selectedUpgrade = availableCards[randomIndex];

            // Удаляем из временного списка, чтобы эта карточка больше не выпала в текущем цикле
            availableCards.RemoveAt(randomIndex);

            // Спавним визуальный объект карточки в контейнер
            GameObject cardGo = Instantiate(cardPrefab, cardsContainer);
            spawnedCards.Add(cardGo);

            // Настраиваем карточку данными
            UpgradeCardUI cardUI = cardGo.GetComponent<UpgradeCardUI>();
            if (cardUI != null)
            {
                cardUI.Setup(selectedUpgrade);
            }
        }
    }

    private void ClearOldCards()
    {
        foreach (GameObject card in spawnedCards)
        {
            if (card != null)
            {
                Destroy(card);
            }
        }
        spawnedCards.Clear();
    }
}
