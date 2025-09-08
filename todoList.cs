using System;
using System.Collections.Generic; 



class Menu
{
    public static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("[1] Yeni görev ekle");
        Console.WriteLine("[2] Görevleri listele");
        Console.WriteLine("[3] Görev güncelle");
        Console.WriteLine("[4] Görev sil");
        Console.WriteLine("[5] Durum değiştir");
        Console.WriteLine("[6] Filtrele/ara");
        Console.WriteLine("[0] Çıkış");
        Console.WriteLine("----------------------");
        Console.WriteLine("Görev -> Başlık, Açıklama, Durum, Öncelik, Bitiş Tarihi, Oluşturulma Tarihi");
        Console.WriteLine("Durumlar -> Todo, Inprogress, Done");
        Console.WriteLine();
    }
}

class Tasks
{
    public string gorevBaslık = "";
    public string gorevAcıklama = "";
    public int gorevDurum;
    public int gorevOncelik;
    public string gorevBaslangic = "";
    public string gorevBitis = "";


    public void TaskOlustur()
    {
        Console.Write("Görev Başlığı: ");
        gorevBaslık = Console.ReadLine();

        Console.Write("Görev Açıklaması: ");
        gorevAcıklama = Console.ReadLine();

        Console.Write("Durum seç (1=Todo, 2=Inprogress, 3=Done): ");
        gorevDurum = int.Parse(Console.ReadLine());

        Console.Write("Öncelik seç (1=Low, 2=Medium, 3=High): ");
        gorevOncelik = int.Parse(Console.ReadLine());

        gorevBaslangic = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

        Console.Write("Bitiş tarihi (gg.aa.yyyy): ");
        gorevBitis = Console.ReadLine();

    }

    public void Guncelle()
    {
        Console.Write($"Yeni Başlık (eski: {gorevBaslık}): ");
        string yeniBaslik = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(yeniBaslik))
            gorevBaslık = yeniBaslik;   



        Console.Write($"Yeni Açıklama (eski: {gorevAcıklama}): "); 
    string yeniAciklama = Console.ReadLine();                 
    if (!string.IsNullOrWhiteSpace(yeniAciklama))             
        gorevAcıklama = yeniAciklama;                        

   
    Console.Write($"Yeni Durum (1=Todo, 2=Inprogress, 3=Done, eski: {gorevDurum}): ");
    string d = Console.ReadLine();                            
    if (d == "1" || d == "2" || d == "3")                     
        gorevDurum = int.Parse(d);                           

 
    Console.Write($"Yeni Öncelik (1=Low, 2=Medium, 3=High, eski: {gorevOncelik}): ");
    string o = Console.ReadLine();                             
    if (o == "1" || o == "2" || o == "3")                      
        gorevOncelik = int.Parse(o);                           

  
    Console.Write($"Yeni Bitiş Tarihi (eski: {gorevBitis}): ");
    string yeniBitis = Console.ReadLine();                     
    if (!string.IsNullOrWhiteSpace(yeniBitis))                 
        gorevBitis = yeniBitis;                               

    Console.WriteLine("\nGörev güncellendi!");    
    }


}

class Program {
        static List<Tasks> gorevListesi = new List<Tasks>();

    static void Main(string[] args)
    {
        while (true)
        {
            Menu.ShowMenu();
            Console.Write("Seçim yap: ");
            string secim = Console.ReadLine();



            switch (secim)
            {
                case "1":
                    var yeni = new Tasks();
                    yeni.TaskOlustur();
                    gorevListesi.Add(yeni);
                    Console.WriteLine("Görev eklendi!");
                    Pause();
                    break;




                       case "2":
                    if (gorevListesi.Count == 0) 
                    {
                        Console.WriteLine("Hiç görev yok.");
                    }

                    else
                    {
                        foreach (var g in gorevListesi)
                        {
                            Console.WriteLine(
                                $"Başlık: {g.gorevBaslık}, " +
                                $"Açıklama: {g.gorevAcıklama}, " +
                                $"Durum: {g.gorevDurum}, " +
                                $"Öncelik: {g.gorevOncelik}, " +
                                $"Başlangıç: {g.gorevBaslangic}, " +
                                $"Bitiş: {g.gorevBitis}"
                            );
                        }
                    }
                    Pause();
                    break;

                case"3":
                    if (gorevListesi.Count == 0)
                    {
                        Console.WriteLine("guncellenecek hiç gorev yok");
                    }
                    else
                    {
                        for (int i = 0; i < gorevListesi.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {gorevListesi[i].gorevBaslık}");
                        }
                        Console.Write("Hangi görevi güncellemek istiyorsun (numara gir): ");
                        int secilenIndex = int.Parse(Console.ReadLine());
                        int index = secilenIndex - 1;







                    
                        if (index >= 0 && index < gorevListesi.Count)
                        {
                            gorevListesi[index].Guncelle();
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim!");
                        }
                    }
                    Pause();
                    break;

                case "0":
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        Pause();
                        break;
                    }
        }
    }

    static void Pause()
    {
        Console.WriteLine("\nDevam etmek için Enter'a bas...");
        Console.ReadLine();
    }
}
