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
    public string GorevBaslik = "";
    public string GorevAciklama = "";
    public int GorevDurum;
    public int GorevOncelik;
    public string GorevBaslangic = "";
    public string GorevBitis = "";

    public void TaskOlustur()
    {
        Console.Write("Görev Başlığı: ");
        GorevBaslik = Console.ReadLine();

        Console.Write("Görev Açıklaması: ");
        GorevAciklama = Console.ReadLine();

        Console.Write("Durum seç (1=Todo, 2=Inprogress, 3=Done): ");
        GorevDurum = int.Parse(Console.ReadLine());

        Console.Write("Öncelik seç (1=Low, 2=Medium, 3=High): ");
        GorevOncelik = int.Parse(Console.ReadLine());

        GorevBaslangic = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

        Console.Write("Bitiş tarihi (gg.aa.yyyy): ");
        GorevBitis = Console.ReadLine();
    }







    public void Guncelle()
    {
        Console.Write($"Yeni Başlık (eski: {GorevBaslik}): ");
        string yeniBaslik = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(yeniBaslik))
            GorevBaslik = yeniBaslik;

        Console.Write($"Yeni Açıklama (eski: {GorevAciklama}): ");
        string yeniAciklama = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(yeniAciklama))
            GorevAciklama = yeniAciklama;

        Console.Write($"Yeni Durum (1=Todo, 2=Inprogress, 3=Done, eski: {GorevDurum}): ");
        string durumSecimi = Console.ReadLine();
        if (durumSecimi == "1" || durumSecimi == "2" || durumSecimi == "3")
            GorevDurum = int.Parse(durumSecimi);

        Console.Write($"Yeni Öncelik (1=Low, 2=Medium, 3=High, eski: {GorevOncelik}): ");
        string oncelikSecimi = Console.ReadLine();
        if (oncelikSecimi == "1" || oncelikSecimi == "2" || oncelikSecimi == "3")
            GorevOncelik = int.Parse(oncelikSecimi);

        Console.Write($"Yeni Bitiş Tarihi (eski: {GorevBitis}): ");
        string yeniBitis = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(yeniBitis))
            GorevBitis = yeniBitis;




        Console.WriteLine("\nGörev güncellendi!");
    }






    
}

class Program
{
    static List<Tasks> _gorevListesi = new List<Tasks>();

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
                    var yeniGorev = new Tasks();
                    yeniGorev.TaskOlustur();
                    _gorevListesi.Add(yeniGorev);
                    Console.WriteLine("Görev eklendi!");
                    Pause();
                    break;

                case "2":
                    if (_gorevListesi.Count == 0)
                    {
                        Console.WriteLine("Hiç görev yok.");
                    }
                    else
                    {
                        foreach (var gorev in _gorevListesi)
                        {
                            Console.WriteLine(
                                $"Başlık: {gorev.GorevBaslik}, " +
                                $"Açıklama: {gorev.GorevAciklama}, " +
                                $"Durum: {gorev.GorevDurum}, " +
                                $"Öncelik: {gorev.GorevOncelik}, " +
                                $"Başlangıç: {gorev.GorevBaslangic}, " +
                                $"Bitiş: {gorev.GorevBitis}"
                            );
                        }
                    }
                    Pause();
                    break;

                case "3":
                    if (_gorevListesi.Count == 0)
                    {
                        Console.WriteLine("guncellenecek hiç gorev yok");
                    }
                    else
                    {
                        for (int i = 0; i < _gorevListesi.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {_gorevListesi[i].GorevBaslik}");
                        }
                        Console.Write("Hangi görevi güncellemek istiyorsun (numara gir): ");
                        int secilenIndex = int.Parse(Console.ReadLine());
                        int index = secilenIndex - 1;

                        if (index >= 0 && index < _gorevListesi.Count)
                        {
                            _gorevListesi[index].Guncelle();




                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim!");
                        }
                    }
                    Pause();
                    break;

                // [4] Görev sil
                case "4":
                    if (_gorevListesi.Count == 0)
                    {
                        Console.WriteLine("Silinecek hiç görev yok.");
                    }
                    else
                    {
                        for (int i = 0; i < _gorevListesi.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {_gorevListesi[i].GorevBaslik}");
                        }
                        Console.Write("Hangi görevi silmek istiyorsun (numara gir): ");
                        int silinecek = int.Parse(Console.ReadLine());
                        int silIndex = silinecek - 1;






                        if (silIndex >= 0 && silIndex < _gorevListesi.Count)
                        {
                            Console.WriteLine($"\"{_gorevListesi[silIndex].GorevBaslik}\" silindi.");
                            _gorevListesi.RemoveAt(silIndex);
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim!");
                        }
                    }
                    Pause();
                    break;

                // [5] Durum değiştir
                case "5":
                    if (_gorevListesi.Count == 0)
                    {
                        Console.WriteLine("Durumu değiştirilecek görev yok.");
                    }
                    else
                    {
                        for (int i = 0; i < _gorevListesi.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {_gorevListesi[i].GorevBaslik} (Durum: {_gorevListesi[i].GorevDurum})");
                        }
                        Console.Write("Hangi görevin durumunu değiştireceksin (numara gir): ");
                        int dInd = int.Parse(Console.ReadLine());
                        int dIdx = dInd - 1;





                        if (dIdx >= 0 && dIdx < _gorevListesi.Count)
                        {
                            Console.Write("Yeni durum (1=Todo, 2=Inprogress, 3=Done): ");
                            string yeni = Console.ReadLine();
                            if (yeni == "1" || yeni == "2" || yeni == "3")





                            {
                                _gorevListesi[dIdx].GorevDurum = int.Parse(yeni);
                                Console.WriteLine("Durum güncellendi.");
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz durum!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim!");
                        }
                    }
                    Pause();
                    break;

                // [6] Filtrele/ara
                case "6":
                    if (_gorevListesi.Count == 0)
                    {
                        Console.WriteLine("Listelenecek görev yok.");
                    }
                    else
                    {
                        Console.WriteLine("Filtreleme türü seç:");

                        Console.WriteLine("1) Duruma göre (1=Todo, 2=Inprogress, 3=Done)");
                        Console.WriteLine("2) Başlık/Açıklama içinde ara (metin)");
                        Console.WriteLine("3) Önceliğe göre (1=Low, 2=Medium, 3=High)");
                        Console.Write("Seçim: ");
                        string f = Console.ReadLine();

                        List<Tasks> sonuc = new List<Tasks>();

                        if (f == "1")
                        {
                            Console.Write("Durum (1/2/3): ");
                            int durum = int.Parse(Console.ReadLine());
                            foreach (var g in _gorevListesi)
                                if (g.GorevDurum == durum) sonuc.Add(g);
                        }
                        else if (f == "2")
                        {
                            Console.Write("Aranacak metin: ");
                            string txt = Console.ReadLine()?.ToLower() ?? "";
                            foreach (var g in _gorevListesi)
                            {
                                if ((g.GorevBaslik ?? "").ToLower().Contains(txt) ||
                                    (g.GorevAciklama ?? "").ToLower().Contains(txt))
                                {
                                    sonuc.Add(g);
                                }
                            }
                        }










                        else if (f == "3")
                        {
                            Console.Write("Öncelik (1/2/3): ");
                            int oncelik = int.Parse(Console.ReadLine());
                            foreach (var g in _gorevListesi)
                                if (g.GorevOncelik == oncelik) sonuc.Add(g);
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim!");
                            Pause();
                            break;
                        }

                        if (sonuc.Count == 0)
                        {
                            Console.WriteLine("Eşleşen görev bulunamadı.");
                        }
                        else
                        {








                            foreach (var gorev in sonuc)
                            {
                                Console.WriteLine(
                                    $"Başlık: {gorev.GorevBaslik}, " +
                                    $"Açıklama: {gorev.GorevAciklama}, " +
                                    $"Durum: {gorev.GorevDurum}, " +
                                    $"Öncelik: {gorev.GorevOncelik}, " +
                                    $"Başlangıç: {gorev.GorevBaslangic}, " +
                                    $"Bitiş: {gorev.GorevBitis}"
                                );
                            }
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
