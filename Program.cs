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


// Partial Class Nedir?
// Normalde bir class tek bir .cs dosyasında tanımlanır. Ama partial anahtar kelimesi kullanırsan, aynı class’ı birden fazla dosyaya bölerek yazabilirsin.Derleyici derleme aşamasında bu parçaları birleştirir ve sanki tek bir class yazmışsın gibi davranır.


//EmployeeProps.cs
public partial class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}

//EmployeeMethods.cs
public partial class Employee
{
    public void Display()
    {
        Console.WriteLine($"Id: {Id}, Name: {Name}");
    }
}

//Mainde
class Program
{
    static void Main()
    {
        Employee emp = new Employee { Id = 1, Name = "Kerem" };
        emp.Display(); 
    }
}


//Statik bir sınıf:Nesne oluşturulamaz.
//İçindeki tüm üyeler (alanlar, metotlar vb.) statik olmalıdır.
//Örnek üye veya yapıcı içeremez.Miras alınamaz; zincirleme kalıtım yapılamaz.
//Bellekte uygulama boyu boyunca kalır (uygulama domain’i boyunca) Örnek:
public static class Calculator
{
    private static int _resultStorage = 0;
    public static string Type = "Arithmetic";
    public static int Sum(int num1, int num2)
    {
        return num1 + num2;
    }
    public static void Store(int result)
    {
        _resultStorage = result;
    }
}


//Jagged Array :(düzensiz dizi) = dizilerin dizisi demektir.Normal çok boyutlu diziden farkı: Her satır farklı uzunlukta olabilir.Örnek:

int[][] jagged = new int[3][];
jagged[0] = new int[] { 1, 2 }; //satır → 2 elemanlı
jagged[1] = new int[] { 3, 4, 5, 6 }; //satır → 4 elemanlı
jagged[2] = new int[] { 7, 8, 9 }; //satır → 3 elemanlı


//Indexer:bir sınıfın ya da struct’ın içinde tanımlanır ve o sınıfın nesnelerine dizi gibi [] ile erişmeyi sağlar.

class MyCollection
{
    private string[] data = new string[3];

    // Indexer tanımı
    public string this[int index]
    {
        get { return data[index]; }   // okuma
        set { data[index] = value; }  // yazma
    }
}

class Program
{
    static void Main()
    {
        MyCollection c = new MyCollection();

        c[0] = "Hello";       // aslında data[0] = "Hello"
        c[1] = "World";       // aslında data[1] = "World"

        Console.WriteLine(c[0]); // Hello
        Console.WriteLine(c[1]); // World
    }
}


//Generics Nedir?
//C#’ta generic tip bağımsız kod yazmaya yarar.Yani aynı sınıfı ya da metodu hem int hem string hem de başka türlerle kullanabilirsin.
//Bunu yaparken bir tip parametresi kullanılır → genelde T harfiyle gösterilir.Type (tip) 
//Yani T bir türün yer tutucusudur. Sen kullanırken T yerine int, string, double vs koyarsın.Örnek:

class DataStore
{
    public int Data { get; set; }
}
//Bu sınıf sadece int için çalışır.
//Eğer string saklamak isteseydin, ikinci bir sınıf yazman gerekirdi.
//Generic ile

class DataStore<T>
{
    public T Data { get; set; }
}

var intStore = new DataStore<int>();
intStore.Data = 42;       // T burada int oldu

var stringStore = new DataStore<string>();
stringStore.Data = "Merhaba"; // T burada string oldu

//Generic Metot:Aynı mantık metotlarda da var:
public static void Print<T>(T value)
{
    Console.WriteLine(value);
}
Kullanım:
Print<int>(100);      // T = int
Print<string>("Hi");  // T = string
Print(3.14);          // T = double (otomatik algılar)


//Generic Constraints:
//Generics ile T her türlü tip olabilir ama bazen T için sınırlama koymak isteyebilirsin.
//o zaman constraint kullanılır. where anahtar kelimesiyle kullanılır.
//Yani “Bu generic sadece şu türlerle çalışabilir” demek.

//Örnek 1 – Referans tip kısıtı

class DataStore<T> where T : class
{
    public T Data { get; set; }
}
//Burada T sadece referans tip (class, string, List<int> vb.) olabilir.Eğer int gibi değer tipi verirsen hata alırsın.
// Örnek 2 – Değer tip kısıtı

class Calculator<T> where T : struct
{
    public T Number { get; set; }
}
//struct constraint → sadece value type (int, double, bool vb.) kabul eder.string veya class verirsen hata olur.


//C#ta 2 tür collection vardır:
//1-Generic Collections:Sadece belirli bir tip tutar.Örn:  
List<int> sayilar = new List<int>();
sayilar.Add(10);
List<string> isimler = new List<string>();
isimler.Add("Ali");

//Dictionary: 
Dictionary<int, string> plakalar = new Dictionary<int, string>();
plakalar.Add(41, "Kocaeli");

//HashSet<T> :Tekrarsız (unique) elemanları saklar.Kümeler (matematikteki set) gibidir.
HashSet<int> set = new HashSet<int> { 1, 2, 2, 3 };
Console.WriteLine(set.Count); // 3 (tekrar yok)

//Queue<T>:FIFO mantığı,ilk giren ilk çıkar. tip güvenli.
Queue<string> q = new Queue<string>();
q.Enqueue("Ali");
q.Enqueue("Veli");
Console.WriteLine(q.Dequeue()); // Ali

Stack<T>:LIFO mantığı,son giren ilk çıkar tip güvenli.
Stack<string> s = new Stack<string>();
s.Push("ilk");
s.Push("son");
Console.WriteLine(s.Pop()); // son

//LinkedList<T>:Çift yönlü bağlı listedir.Ortadan ekleme/çıkarma hızlıdır.
LinkedList<string> ll = new LinkedList<string>();
ll.AddLast("A");
ll.AddLast("B");
ll.AddFirst("C"); // Baştan ekleme

//SortedSet<T>:HashSet gibi tekrarsız eleman tutar, ayrıca sıralıdır.
SortedSet<int> ss = new SortedSet<int>() { 3, 1, 2, 2 };
foreach (var i in ss) Console.WriteLine(i); // 1,2,3

//2-Non-Generic Collections:Farklı tipleri aynı listede saklayabilir.
//1-: ArrayList 
var arlist2 = new ArrayList()
                {
                    2, "Steve", " ", true, 4.5, null
                };

//Hashtable:Key–Value (anahtar-değer) yapısıdır.Key benzersizdir.
Hashtable ht = new Hashtable();
ht.Add(1, "Ali");
ht.Add("id", 42);
//kalanlar da benzer çalışır



//| Kategori           | Önerilen Standart                                    |
//| ------------------ | ---------------------------------------------------- |
//| Sınıf/Metot Adları | PascalCasing (`ClientActivity`)                      |
//| Yerel Değişkenler  | camelCasing (`itemCount`)                            |
//| Identifiers        | Tip göstergesi yok (`counter`, `name`)               |
//| Sabitler           | Normal case (`ShippingType`), all caps kaçar         |
//| Kısaltmalar        | Kaçınılmalı; yaygın şekilde izinli                   |
//| Alt Çizgi          | `private static _member` dışında kaçınılmalı         |
//| Interface          | `I` ile başla (`IShape`)                             |


//OOP:

//CONSTRUCTOR yani yapıcı metotlar classlardan obje oluşturulduğu sırada olusan geriye dönüş tipi olmayan metotlardır.Aynı class ismiyle oluşturulur.


Araba araba1 = new Araba(); //yazdığında olay şu şekilde gerçekleşiyor:
	//new Araba() kısmı:Bellekte Araba sınıfından bir nesne oluşturulur.Bu sırada sınıfın constructor’ı (yapıcı metodu) çalıştırılır. Yani public Araba() gibi bir constructor varsa, içindeki kod tetiklenir.
	//araba1 = ... kısmı:Oluşturulan bu yeni nesnenin referansı (new Araba() sonucu dönen adres) araba1 değişkenine atanır.Böylece artık araba1, o nesneyi işaret eder.Yani özetle:new Araba() → Nesneyi oluşturur ve constructor’u çalıştırır.Araba araba1 = → Oluşturulan nesnenin referansını araba1 değişkenine bağlar.


//C#’ta erişim belirleyiciler (access modifiers):bir sınıfın, metodun, alanın veya özelliğin nereden erişilebileceğinibelirler. Yani “bu üyeyi kimler görebilir, kimler kullanabilir” sorusunun cevabını verir.
Başlıca erişim belirleyiciler şunlardır:

//1. public:Her yerden erişilebilir.Aynı proje içinde veya dışarıdan (başka assembly’den) görülebilir.
//2. private:Sadece tanımlandığı sınıfın içinden erişilebilir.
//3. protected:Tanımlandığı sınıf içinde ve ondan türeyen (inherit edilen) alt sınıflarda erişilebilir.
//4. internal:Aynı assembly/proje içinde erişilebilir.Farklı projelerden (dll/exe) erişilemez.





//ekstra not:Static constructor nedir?
//Bir sınıf ilk defa kullanılmaya başlandığında çalışan özel bir metottur.Amaç: static değişkenleri başlatmak.
//Sadece 1 kez çalışır.Sen çağırmazsın → .NET otomatik çağırır.
//Parametre alamaz.
//public/private yazılmaz. (derleyici zaten private kabul eder)
//Normal constructor gibi nesne için değil, sınıf için çalışır.

class Araba
{
    public static int ArabaSayisi;

    // static constructor
    static Araba()
    {
        Console.WriteLine("Static constructor çalıştı!");
        ArabaSayisi = 0;
    }

    // normal constructor
    public Araba()
    {
        ArabaSayisi++;
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine(Araba.ArabaSayisi); // static constructor burada tetiklenir
        Araba a1 = new Araba(); // normal constructor
        Araba a2 = new Araba(); // normal constructor
        Console.WriteLine("Toplam araba sayısı: " + Araba.ArabaSayisi);
    }
}

//static constructor → sınıf ilk defa kullanıldığında, 1 kere.
//normal constructor → her new yaptığında, tekrar tekrar.

//ENCAPSULATION :Bir sınıfın iç detaylarını (alanlar, veriler) dış dünyadan gizleyip, sadece gerekli olan kısımları kontrollü şekilde açmaya denir.
//Set ıle degerler atanır get ıle alınır


//Field (Alan):Bir class içindeki değişkene Field denir.Direkt değeri tutar. Örnek:

class Student
{
    public int id;  // field
}



//Property (Özellik):Field’ın kontrollü erişim kapısıdır.get → Değeri döndürür.set → Değeri atar.
//İçine ekstra kurallar/şartlar koyabilirsin.Örnek:

class Student
{
    private int id;  // field (gizli)

    public int StudentId   // property
    {
        get { return id; }       // okumak için
        set { if (value > 0)     // yazarken kural
                 id = value; }
    }
}



//ÖRNEK2:
class Student
{
    private int id;  // field (gizli değişken)

    // Property
    public int StudentId
    {
        get { return id; }       // okumak için
        set {                   // yazmak için
            if (value > 0)       // sadece pozitif olursa atansın
                id = value;
        }
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student();

        s.StudentId = -5;   // kural izin vermez
        s.StudentId = 10;   //geçerli
        Console.WriteLine(s.StudentId); // 10
    }
}



//Inheritance:Bir sınıfın (base class / parent class) özelliklerini ve metotlarını başka bir sınıfa (derived class / child class) aktarmasıdır.
class Hayvan
{
    public void NefesAl() => Console.WriteLine("Hayvan nefes alıyor.");
}

class Kedi : Hayvan
{
    public void Miyavla() => Console.WriteLine("Miyav!");
}

class Program
{
    static void Main()
    {
        Kedi k = new Kedi();
        k.NefesAl(); // miras aldığı için kullanabilir
        k.Miyavla(); // kendi metodu
    }
}

//operatörü ile miras alınır → class Araba : Arac.
//Bir sınıf sadece 1 sınıftan miras alabilir (C#’ta çoklu inheritance yoktur).Ama birden fazla interface’ten miras alınabilir.
//Alt sınıf üst sınıfın:public ve protected üyelerine erişebilir,private üyelerine erişemez.
//Üst sınıfın constructor’ı, alt sınıfın constructor’ından önce çalışır.
	

//override:bir alt sınıfın (derived), üst sınıfta (base) tanımlanmış bir metodu kendi ihtiyacına göre yeniden yazmasıdır.
//Ama override edebilmen için, üst sınıftaki metodun virtual, abstract veya override olması gerekir.



//Base Class

class Hayvan
{
    public virtual void SesCikar()
    {
        Console.WriteLine("Bir hayvan ses çıkardı.");
    }
}
//Derived Classes

class Kedi : Hayvan
{
    public override void SesCikar()
    {
        Console.WriteLine("Miyav!");
    }
}

class Kopek : Hayvan
{
    public override void SesCikar()
    {
        Console.WriteLine("Hav hav!");
    }
}
//Kullanım

class Program
{
    static void Main()
    {
        Hayvan h1 = new Kedi();
        Hayvan h2 = new Kopek();

        h1.SesCikar(); // Miyav!
        h2.SesCikar(); // Hav hav!
    }
}




//virtual:Bir metodu alt sınıfta değiştirebilmek için işaretlersin.Yani “bu metodu ister aynen kullan, ister değiştir” demektir.Gövdesi vardır.


class Hayvan
{
    public virtual void SesCikar()
    {
        Console.WriteLine("Hayvan ses çıkardı.");
    }
}

class Kedi : Hayvan
{
    public override void SesCikar()
    {
        Console.WriteLine("Miyav!");
    }
}
//Burada SesCikar() üst sınıfta tanımlı ama Kedi kendi versiyonunu yazıyor.

//abstract:Gövdesiz metot demektir.(Metotların Yapısı şöyledir:
//Bir metot aslında iki parçadan oluşur:
//İmza (signature) → Metodun adı, dönüş tipi ve parametreleri.
//Gövde (body) → { } süslü parantez içindeki çalışan kodlar.
								  
//Gövdeli (Normal Metot):Hem imzası vardır hem de içinde çalışan kod vardır.Yani “ne yapacağını ben yazdım” der.
		public void MerhabaDe()
		{
		    Console.WriteLine("Merhaba!"); // <- gövde burası
		}//Burada metodun ne yapacağı belli → ekrana "Merhaba!" yazdırıyor.
	 Gövdesiz (Abstract Metot):Sadece imzası vardır ama içinde kod yoktur.Yani “bu metodun nasıl yapılacağını ben değil, alt sınıf yazsın” der.

		 public abstract void SesCikar(); // <- gövde yok
//Burada metodun adı belli (SesCikar), ama nasıl çalışacağı belirsiz. Alt sınıf bu metodu override ederek doldurmak zorunda.
//Sadece abstract class içinde tanımlanabilir.
//Alt sınıflar bu metodu zorunlu olarak override etmek zorundadır.
//Örnek:

abstract class Hayvan
{
    public abstract void SesCikar(); // gövdesiz
}

class Kedi : Hayvan
{
    public override void SesCikar()
    {
        Console.WriteLine("Miyav!");
    }
}

class Kopek : Hayvan
{
    public override void SesCikar()
    {
        Console.WriteLine("Hav hav!");
    }
}
Burada Hayvan sınıfının SesCikar metodu zorunlu olarak her alt sınıfta yeniden yazılmak zorunda.

	

//Abstract Class Nedir?:Abstract class’tan nesne oluşturamazsın.
//Amaç: “Benzer şeyleri gruplandırmak, ortak davranışları yazmak, özel kısımları alt sınıflara bırakmak.”

//İçinde abstract metotlar (gövdesiz, sadece adı olan) ve normal metotlar bulunabilir.

// Neden Abstract Class Kullanırız?
//Alt sınıflara zorunlu görevler yüklemek için.
//Mesela her hayvan ses çıkarmalıdır (MakeSound()), ama nasıl çıkardığı farklıdır.
//Ortak davranışları (ör. Eat()) tek yerde toplamak için.Daha düzenli ve kurallı bir yapı kurmak için.

	
abstract class Animal
{
    public abstract void MakeSound(); // Gövdesiz, alt sınıflar yazmak zorunda
    public void Eat()  // Normal metot
    {
        Console.WriteLine("Hayvan yemek yiyor.");
    }
}


//Abstract class’tan direkt nesne oluşturamazsın:
// Hata verir:

var a = new Animal(); // olmaz

//Ama ondan türeyen (inherit eden) sınıflar kullanılır:
class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Hav hav!");
    }
}

class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Miyav!");
    }
}

Animal d = new Dog();
d.MakeSound(); // "Hav hav!"
d.Eat();       // "Hayvan yemek yiyor."






//Interface Nedir:Interface (arayüz), bir sınıfın hangi davranışlara sahip olması gerektiğini tanımlar.
//İçinde gövdesiz metotlar (imzalar) bulunur.
//Nesne oluşturulamaz.
//Interface’i implement eden sınıf, içindeki tüm metotları yazmak zorundadır.
//Örnek:

interface IFlyable
{
    void Fly(); // Gövdesiz, sadece imza
}

class Bird : IFlyable
{
    public void Fly()
    {
        Console.WriteLine("Kuş kanat çırparak uçar.");
    }
}

class Plane : IFlyable
{
    public void Fly()
    {
        Console.WriteLine("Uçak motor gücüyle uçar.");
    }
}


//Burada:Hem Bird hem de Plane, uçabilme özelliği kazanmış oldu.Ama “nasıl uçtukları” farklı.


👉 Örnek:
abstract class Animal
{
    public abstract void MakeSound();
    public void Eat() => Console.WriteLine("Hayvan yemek yiyor.");
}

interface IFlyable
{
    void Fly();
}

class Bird : Animal, IFlyable
{
    public override void MakeSound() => Console.WriteLine("Cik cik!");
    public void Fly() => Console.WriteLine("Kuş uçuyor!");
}

