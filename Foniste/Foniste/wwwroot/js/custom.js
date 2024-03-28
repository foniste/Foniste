// not : getElementById() metodu elementi bulamazsa null return eder.
let hizliErisim = document.getElementById("hizli-erisim");
let hizliErisimAcilirMenu = document.getElementById("hizli-erisim-acilir-menu");
//let headerKusak = document.getElementById("master-header-kusak");
let header = document.getElementById("master-header");
let logo = document.getElementById("master-header-logo");
let basaDonButonu = document.getElementById("basa-don");
let slider = document.getElementById("slider");
//let duyurularVeEtkinlikler = document.getElementById("duyurular-etkinlikler");
// "tanitim" id'sine sahip ��eyi se�me
let tanitim = document.getElementById("tanitim");
let haberler = document.getElementById("haberler");
let hamburgerMenu = document.getElementById("hamburger-menu");
let hamburgerMenuAcilirMenu = document.getElementById("hamburger-menu-acilir-menu");

let headerUzunluk = /*headerKusak.offsetHeight +*/ header.offsetHeight; //header'in toplam uzunlu�u.

//DUSTAT�ST�K SAYA� ��LEMLER�
//slider, duyurular, etkinlikler ve haberler her sayfada yok. dolay�s�yla hata ile kar��la�mamak i�in bunlar var m� diye �n kontrol yapal�m.
if (slider != null && tanitim != null && haberler != null) {
	let sliderUzunluk = slider.offsetHeight;
	let tanitimUzunluk = tanitim.offsetHeight;
	let haberlerUzunluk = haberler.offsetHeight;

	function dustatistikSayac() {
		let suAnkiKonum = window.pageYOffset || document.documentElement.scrollTop;
		Uzunluk = headerUzunluk + sliderUzunluk + tanitimUzunluk + haberlerUzunluk;
		if (suAnkiKonum >= Uzunluk -300) {
			$('.count').counterUp({
				delay: 10,
				time: 2000
			});
			window.removeEventListener("scroll", dustatistikSayac);
		}
	}
}

hizliErisim.onclick = function () { //h�zl� eri�im linkine t�kland���nda :
	if (hizliErisimAcilirMenu.style.display === "block") { //h�zl� eri�im a��l�r men�s� zaten a��ksa :
		hizliErisimAcilirMenu.style.display = "none"; //h�zl� eri�im a��l�r men�s�n� kapat.
		hizliErisim.style.borderBottom = "none"; //h�zl� eri�im linki alt�ndaki border efektini kald�r.
	}
	else { //h�zl� eri�im a��l�r men�s� a��k de�ilse :
		hizliErisimAcilirMenu.style.display = "block"; //h�zl� eri�im a��l�r men�s�n� a�.
		hizliErisim.style.borderBottom = "3px solid #FFF"; //h�zl� eri�im linki alt�na border efekti ekle.
	}
};

hamburgerMenu.onclick = function () {
	if (hamburgerMenuAcilirMenu.style.display === "block") {
		hamburgerMenuAcilirMenu.style.display = "none";
	}
	else {
		hamburgerMenuAcilirMenu.style.display = "block";
	}
}

function seffafHeader() {
	var suAnkiKonum = window.pageYOffset || document.documentElement.scrollTop;
	if (suAnkiKonum >= headerUzunluk) { // �u anki konum header uzunlu�undan b�y�k veya e�itse :
		header.classList.add("seffafHeader"); //header �effaf olsun.
		logo.setAttribute("src", "img/logo.png"); //logo da �effafl��a uygun bi�imde de�i�sin.
		basaDonButonu.style.display = "block"; //ba�a d�n butonu g�z�ks�n.
		hizliErisimAcilirMenu.classList.add("hizli-erisim-seffaf");
		hamburgerMenuAcilirMenu.classList.add("hamburger-menu-acilir-menu-seffaf");
	}
	else {
		header.classList.remove("seffafHeader");
		logo.setAttribute("src", "img/logo-21.png");
		basaDonButonu.style.display = "none";
		hizliErisimAcilirMenu.classList.remove("hizli-erisim-seffaf");
		hamburgerMenuAcilirMenu.classList.remove("hamburger-menu-acilir-menu-seffaf");
	}
}

$(document).ready(function () {// slider�n slide lar� aras�na 20px padding uyguluyor
	$('.carousel').on('slide.bs.carousel', function () {
		$('.carousel .item').css('margin-right', '20px');
	});

	$('.carousel').on('slid.bs.carousel', function () {
		$('.carousel .item').css('margin-right', '0');
	});
});


window.addEventListener("scroll", seffafHeader); //scroll yap�ld�k�a seffafHeader fonksiyonu �a�r�ls�n.
window.addEventListener("scroll", dustatistikSayac); //scroll yap�ld�k�a dustatistikSayac fonksiyonu �a�r�ls�n.