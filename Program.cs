//string C#’ta referans tipi olmasına rağmen immutable (değiştirilemez).
//Yani sen şu işlemi yaptığında:

string s = "Hello";
s += " World";

//İlk önce "Hello" diye bir string nesnesi heap’te oluşur.
//s += " World" yaptığında "Hello World" diye yeni bir string nesnesi oluşturulur.
// string immutable olduğu için her "+" veya Replace() işlemiyle yeni bir nesne oluşur ve bu belleği tüketir. Özellikle döngülerde çok fazla yeni nesne oluşması uygulamayı yavaşlatabilir.
// StringBuilder, tek bir nesne üzerinde değişiklik yaparak bu sorunu çözer. Bellekte tek nesne, içindeki char dizisini genişleterek işler
//Oluşturması şu şekildedir:

using System.Text;

var sb1 = new StringBuilder();                // Boş
var sb2 = new StringBuilder("Hello");         // Başlangıç metni
var sb3 = new StringBuilder("Hi", capacity:50);// Belirlenen kapasite

sb1.Append("World!");
sb2.Insert(5, " C#");
// sb içindeki değeri değiştirmiş olduk, yeni nesne oluşmadı.
// StringBuilder ile yapılan değişiklikleri string tipine çevirmek için ToString() kullanılır.


// METHOD PARAMETER KEYWORDS(yöntem parametre anahtar kelimeleri):
//  1. params:Bir metoda değişken sayıda parametre göndermeni sağlar.Metod içinde bu parametreler bir dizi (array) olarak işlenir.
static void Topla(params int[] sayilar)
{
    int toplam = 0;
    foreach (int s in sayilar)
        toplam += s;
    Console.WriteLine("Toplam = " + toplam);
}

static void Main()
{
    Topla(1, 2, 3);       // Toplam = 6
    Topla(10, 20, 30, 40); // Toplam = 100
}


//  2. ref:Parametreyi referans olarak gönderir.Yani metod içinde değiştirirsen, dışarıdaki değişken de değişir.Kullanabilmek için değişkenin önceden değer almış olması gerekir.
static void Arttir(ref int x)
{
    x += 10;
}

static void Main()
{
    int sayi = 5;
    Arttir(ref sayi);
    Console.WriteLine(sayi); // 15
}


// 3. out:ref’e benzer, ama farkı değişkeni göndermeden önce değer vermek zorunda değilsin.Metodun içinde mutlaka değer atanmak zorunda.
static void Carp(out int sonuc, int a, int b)
{
    sonuc = a * b; // mutlaka değer atanmalı
}

static void Main()
{
    int carpim; // değer vermedim
    Carp(out carpim, 3, 4);
    Console.WriteLine(carpim); // 12
}


// Field-Property

class Student
{
    private int id;  // field 

    public int StudentId   // property
    {
        get { return id; }       // okumak için
        set { if (value > 0)     // yazarken kural
                 id = value; }
    }
}



//var ile önce tanımlayıp sonra değer atayamazsın çünkü C# derleyicisi tipini o ilk değerden çıkarmak zorunda:
var a;
a = 9; #çalısmaz

#Ayrıca metot parametrelerinde var kullanılmaz 
void Display(var param)  //  Geçersiz
{
    Console.WriteLine(param);
}
//Burada derleyici param’ın hangi tip olacağını bilmiyor. Çünkü param’a ilk değer metot çağrılırken gelecek. O yüzden var kullanamazsın.

Class:reference type (heap’te tutulur). Nesne kopyalanınca referans kopyalanır.
Struct:value type (stack’te tutulur). Nesne kopyalanınca verinin kendisi kopyalanır

