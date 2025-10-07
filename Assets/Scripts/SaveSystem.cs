using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveSystem : MonoBehaviour
{
    private static string SAVE_FOLDER;
    public SailingTracker sailingTracker;

    public static void Init(){
    SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER); // creates save folder
        }
    }

    public void devReset()
    {
        if (File.Exists(SAVE_FOLDER + "/saveFight.json"))
        {
            Debug.Log("Fight Culled!");
            File.Delete(SAVE_FOLDER + "/saveFight.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveWelcome.json"))
        {
            Debug.Log("Welcome Culled!");
            File.Delete(SAVE_FOLDER + "/saveWelcome.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveFishGiven.json"))
        {
            Debug.Log("Fish Given Culled!");
            File.Delete(SAVE_FOLDER + "/saveFishGiven.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveIsland.json"))
        {
            Debug.Log("Island Culled!");
            File.Delete(SAVE_FOLDER + "/saveIsland.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveTotalTravel.json"))
        {
            Debug.Log("Total Travel Culled!");
            File.Delete(SAVE_FOLDER + "/saveTotalTravel.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveTravelTime.json"))
        {
            Debug.Log("TravelTime Culled!");
            File.Delete(SAVE_FOLDER + "/saveTravelTime.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveBP.json"))
        {
            Debug.Log("BP Culled!");
            File.Delete(SAVE_FOLDER + "/saveBP.json");
        }
        if (File.Exists(SAVE_FOLDER + "/savePet.json"))
        {
            Debug.Log("Pet Culled!");
            File.Delete(SAVE_FOLDER + "/savePet.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveCollection.json"))
        {
            Debug.Log("Collection Culled!");
            File.Delete(SAVE_FOLDER + "/saveCollection.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveInventory.json"))
        {
            Debug.Log("Inventory Culled!");
            File.Delete(SAVE_FOLDER + "/saveInventory.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveQuests.json"))
        {
            Debug.Log("Quests Culled!");
            File.Delete(SAVE_FOLDER + "/saveQuests.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveFreeChest.json"))
        {
            Debug.Log("FreeChest Culled!");
            File.Delete(SAVE_FOLDER + "/saveFreeChest.json");
        }
        if (File.Exists(SAVE_FOLDER + "/savePaidChest.json"))
        {
            Debug.Log("PaidChest Culled!");
            File.Delete(SAVE_FOLDER + "/savePaidChest.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveSail.json"))
        {
            Debug.Log("Sail Culled!");
            File.Delete(SAVE_FOLDER + "/saveSail.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveFishing.json"))
        {
            Debug.Log("Fishing Culled!");
            File.Delete(SAVE_FOLDER + "/saveFishing.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveTasksCompletedToday.json"))
        {
            Debug.Log("TasksCompletedToday Culled!");
            File.Delete(SAVE_FOLDER + "/saveTasksCompletedToday.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveDabloons.json"))
        {
            Debug.Log("Dabloons Culled!");
            File.Delete(SAVE_FOLDER + "/saveDabloons.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveCharacters.json"))
        {
            Debug.Log("Characters Culled!");
            File.Delete(SAVE_FOLDER + "/saveCharacters.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveResource.json"))
        {
            Debug.Log("Resource Culled!");
            File.Delete(SAVE_FOLDER + "/saveResource.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveHome.json"))
        {
            Debug.Log("Home Culled!");
            File.Delete(SAVE_FOLDER + "/saveHome.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveBaitCollectionStorage.json"))
        {
            Debug.Log("BaitCollection Culled!");
            File.Delete(SAVE_FOLDER + "/saveBaitCollectionStorage.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveDailyChest.json"))
        {
            Debug.Log("DailyChest Culled!");
            File.Delete(SAVE_FOLDER + "/saveDailyChest.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveBaitTimer.json"))
        {
            Debug.Log("BaitTimer Culled!");
            File.Delete(SAVE_FOLDER + "/saveBaitTimer.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveSeagull.json"))
        {
            Debug.Log("Seagull Culled!");
            File.Delete(SAVE_FOLDER + "/saveSeagull.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveStreak.json"))
        {
            Debug.Log("Streak Culled!");
            File.Delete(SAVE_FOLDER + "/saveStreak.json");
        } //Gonna let the streak run for a bit!
        if (File.Exists(SAVE_FOLDER + "/saveFishCount1.json"))
        {
            Debug.Log("FishCount Culled!");
            File.Delete(SAVE_FOLDER + "/saveFishCount1.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveClaimed.json"))
        {
            Debug.Log("Claimed Culled!");
            File.Delete(SAVE_FOLDER + "/saveClaimed.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveRod.json"))
        {
            Debug.Log("Rod Culled!");
            File.Delete(SAVE_FOLDER + "/saveRod.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveCosts.json"))
        {
            Debug.Log("Costs Culled!");
            File.Delete(SAVE_FOLDER + "/saveCosts.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveDecorations.json"))
        {
            Debug.Log("Decorations Culled!");
            File.Delete(SAVE_FOLDER + "/saveDecorations.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveIAP.json"))
        {
            Debug.Log("IAP Culled!");
            File.Delete(SAVE_FOLDER + "/saveIAP.json");
        }
        if (File.Exists(SAVE_FOLDER + "/saveAudio.json")) {
            Debug.Log("Audio Culled!");
            File.Delete(SAVE_FOLDER + "/saveAudio.json");
        }
    }


    public static void SaveFighting(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveFight.json", saveString);
    }
    
    public static void SaveAudio(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveAudio.json", saveString);
    }
    
    public static void SaveWelcome(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "/saveWelcome.json", saveString);
    }
    
    public static void SaveCharacters(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "/saveCharacters.json", saveString);
    }
    
    public static void SaveQuests(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "/saveQuests.json", saveString);
    }

    public static void SaveIAP(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "/saveIAP.json", saveString);
    }

    public static void SaveDecorations(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "/saveDecorations.json", saveString);
    }

    public static void SaveCosts(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveCosts.json", saveString);
    }

    public static void SaveRod(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveRod.json", saveString);
    }

    public static void SaveClaimed(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveClaimed.json", saveString);
    }

    public static void SaveIsland(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveIsland.json", saveString);
    }

    public static void SaveSeagull(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveSeagull.json", saveString);
    }

    public static void SaveTasksCompletedToday(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveTasksCompletedToday.json", saveString);
    }

    public static void SaveBP(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveBP.json", saveString);
    }

    public static void SaveFishGiven(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveFishGiven.json", saveString);
    }

    public static void SaveTravelTime(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveTravelTime.json", saveString);
    }

    public static void SaveTotalTravel(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveTotalTravel.json", saveString);
    }

    public static void SaveDabloons(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveDabloons.json", saveString);
    }

    public static void SaveDailyChest(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveDailyChest.json", saveString);
    }

    public static void SaveBaitTimer(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveBaitTimer.json", saveString);
    }
    
    public static void SaveBaitCollectionStorage(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveBaitCollectionStorage.json", saveString);
    }

    public static void SaveCollection(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveCollection.json", saveString);
    }

    public static void SaveInventory(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveInventory.json", saveString);
    }

    public static void SavePet(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/savePet.json", saveString);
    }

    public static void SaveFreeChest(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveFreeChest.json", saveString);
    }

    public static void SavePaidChest(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/savePaidChest.json", saveString);
    }

    public static void SaveSail(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveSail.json", saveString);
    }

    public static void SaveFishing(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveFishing.json", saveString);
    }

    public static void SaveResource(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveResource.json", saveString);
    }

    public static void SaveStreak(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveStreak.json", saveString);
    }

    public static void SaveHome(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveHome.json", saveString);
    }
    public static void SaveFishCount1(string saveString){
        File.WriteAllText(SAVE_FOLDER + "/saveFishCount1.json", saveString);
    }

    public static string LoadFighting(){
        if (File.Exists(SAVE_FOLDER + "/saveFight.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveFight.json");
            return saveString;
        } else{
            return null;
        }
    }
    
    public static string LoadWelcome(){
        if (File.Exists(SAVE_FOLDER + "/saveWelcome.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveWelcome.json");
            return saveString;
        } else{
            return null;
        }
    }
    
    public static string LoadQuests()
    {
        if (File.Exists(SAVE_FOLDER + "/saveQuests.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveQuests.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }
    
    public static string LoadAudio()
    {
        if (File.Exists(SAVE_FOLDER + "/saveAudio.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveAudio.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string LoadCharacters()
    {
        if (File.Exists(SAVE_FOLDER + "/saveCharacters.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveCharacters.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string LoadDecorations()
    {
        if (File.Exists(SAVE_FOLDER + "/saveDecorations.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveDecorations.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }
    
    public static string LoadIAP()
    {
        if (File.Exists(SAVE_FOLDER + "/saveIAP.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveIAP.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string LoadFishGiven()
    {
        if (File.Exists(SAVE_FOLDER + "/saveFishGiven.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveFishGiven.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string LoadRod(){
        if (File.Exists(SAVE_FOLDER + "/saveRod.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveRod.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadCosts(){
        if (File.Exists(SAVE_FOLDER + "/saveCosts.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveCosts.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadClaimed(){
        if (File.Exists(SAVE_FOLDER + "/saveClaimed.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveClaimed.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadCollection(){
        if (File.Exists(SAVE_FOLDER + "/saveCollection.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveCollection.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadDabloons(){
        if (File.Exists(SAVE_FOLDER + "/saveDabloons.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveDabloons.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadSeagull(){
        if (File.Exists(SAVE_FOLDER + "/saveSeagull.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveSeagull.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadBP(){
        if (File.Exists(SAVE_FOLDER + "/saveBP.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveBP.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadPet(){
        if (File.Exists(SAVE_FOLDER + "/savePet.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/savePet.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadTasksCompletedToday(){
        if (File.Exists(SAVE_FOLDER + "/saveTasksCompletedToday.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveTasksCompletedToday.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadTotalTravel(){
        if (File.Exists(SAVE_FOLDER + "/saveTotalTravel.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveTotalTravel.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadIsland(){
        if (File.Exists(SAVE_FOLDER + "/saveIsland.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveIsland.json");
            return saveString;
        } else{
            return null;
            
        }
    }

    public static string LoadTravelTime(){
        if (File.Exists(SAVE_FOLDER + "/saveTravelTime.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveTravelTime.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadDailyChest(){
        if (File.Exists(SAVE_FOLDER + "/saveDailyChest.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveDailyChest.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadBaitCollectionStorage(){
        if (File.Exists(SAVE_FOLDER + "/saveBaitCollectionStorage.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveBaitCollectionStorage.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadBaitTimer(){
        if (File.Exists(SAVE_FOLDER + "/saveBaitTimer.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveBaitTimer.json");
            return saveString;
        } else{
            return null;
        }
    }

    public static string LoadHome(){
        if (File.Exists(SAVE_FOLDER + "/saveHome.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveHome.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadSail(){
        if (File.Exists(SAVE_FOLDER + "/saveSail.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveSail.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadInventory(){
        if (File.Exists(SAVE_FOLDER + "/saveInventory.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveInventory.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadFreeChest(){
        if (File.Exists(SAVE_FOLDER + "/saveFreeChest.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveFreeChest.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadPaidChest(){
        if (File.Exists(SAVE_FOLDER + "/savePaidChest.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/savePaidChest.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadResource(){
        if (File.Exists(SAVE_FOLDER + "/saveResource.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveResource.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadFishing(){
        if (File.Exists(SAVE_FOLDER + "/saveFishing.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveFishing.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadStreak(){
        if (File.Exists(SAVE_FOLDER + "/saveStreak.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveStreak.json");
            return saveString;
        }
        else {
            return null;
        }
    }

    public static string LoadFishCount1(){
        if (File.Exists(SAVE_FOLDER + "/saveFishCount1.json")){
            string saveString = File.ReadAllText(SAVE_FOLDER + "/saveFishCount1.json");
            return saveString;
        } else{
            return null;
        }
    }
}
