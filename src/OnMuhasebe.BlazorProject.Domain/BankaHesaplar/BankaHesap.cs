namespace OnMuhasebe.BlazorProject.BankaHesaplar;

public class BankaHesap:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru BankaHesapTuru  { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public string Aciklama { get; set; }
    //Relation'lar
    /*
     * direkt bankada şube açılamaycağı için bir bankanın şübesinde hesap açılacağı için bir bankanın şubesi olacak. banka hesap ile banka şube id arasında ilişki olacak.
     * özel kod alanları -> sebebi uygulamaya esneklik katması. Kullanıcılar farklı özellikler kaydetmek isteyebilirler bu entity'ler ile ilgili.buna özgü sorgular yapmak isteyebilirler.
     * SubeId -> her banka hesabı bir firmanın şubesine aittir, olmak zorundadır.
     * Id alanının olmaması -> bu alanı kalıtım(inheritance) üzerinden alıcaz.
     * Id'lerin guid olmasının sebebi -> guid unique bir değerdir ve uluslarası geçerliliği vardır. Özellikle database'deki table'lardaki verilerin manuel olarak birleştirilmesi durumunda çok fazla işimize yaramaktadır. örneğin iki farklı database'in içeriklerinin aynı olması durumunda, veri tipleri de aynı ise int değer verirsek id'ye her iki db'de de 1'den başladığı için id'ler burada bir çakışma meydana gelir. Tabii bu durum çözülemez değil fakat uğraş gerektiriyor, burada guid yaparsak buna gerek kalmaz. dezavantajı ise sayılardan ve harflerden oluştuğu için string bir değer olmaktadır. Dolayısıyla ilk olarak eklediğimiz guid'in değeri sonra eklenecek olan guid değerden yüksek olabilir. yani sonradan eklenen veriden daha sonradan eklenebilir, sıralamada sorun çıkarabilmektedir. Burada abp framework ise artanlu guid değeri oluşturabiliyor. Yani sıralamada herhangi bir sorun oluşturmuyor.
     * FullAuditedAggregateRoot sınıfını abp framework sağlamaktadır. Audited veriler; ne zaman oluşturuldu, kim tarafından oluşturuldu, ne zaman ve kim tarafından değiştirildi, hangi tarihte oluşturuldu, kim ve ne zaman sildi vb. verilerden oluşmaktadır. table'ları takip etmekle ilgilidir. abp ile gelen bu hazır sınıf sayesinde bu gibi durumları sonradan kod yazmadan otomatik olarak bize bu özellikleri sunmaktadır. Aynı zamanda bu sınıfı kök entity'lerde de kullanırız.
     */
    public Guid BankaSubeId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid SubeId { get; set; }
    //--
    public bool Durum { get; set; }
}