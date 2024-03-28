// not : getElementById() metodu elementi bulamazsa null return eder.
let hizliErisim = document.getElementById("hizli-erisim");
let hizliErisimAcilirMenu = document.getElementById("hizli-erisim-acilir-menu");
//let headerKusak = document.getElementById("master-header-kusak");
let header = document.getElementById("master-header");
let logo = document.getElementById("master-header-logo");
let basaDonButonu = document.getElementById("basa-don");
let slider = document.getElementById("slider");
//let duyurularVeEtkinlikler = document.getElementById("duyurular-etkinlikler");
// "tanitim" id'sine sahip öðeyi seçme
let tanitim = document.getElementById("tanitim");
let haberler = document.getElementById("haberler");
let hamburgerMenu = document.getElementById("hamburger-menu");
let hamburgerMenuAcilirMenu = document.getElementById("hamburger-menu-acilir-menu");

let headerUzunluk = /*headerKusak.offsetHeight +*/ header.offsetHeight; //header'in toplam uzunluðu.

//DUSTATÝSTÝK SAYAÇ ÝÞLEMLERÝ
//slider, duyurular, etkinlikler ve haberler her sayfada yok. dolayýsýyla hata ile karþýlaþmamak için bunlar var mý diye ön kontrol yapalým.
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

hizliErisim.onclick = function () { //hýzlý eriþim linkine týklandýðýnda :
	if (hizliErisimAcilirMenu.style.display === "block") { //hýzlý eriþim açýlýr menüsü zaten açýksa :
		hizliErisimAcilirMenu.style.display = "none"; //hýzlý eriþim açýlýr menüsünü kapat.
		hizliErisim.style.borderBottom = "none"; //hýzlý eriþim linki altýndaki border efektini kaldýr.
	}
	else { //hýzlý eriþim açýlýr menüsü açýk deðilse :
		hizliErisimAcilirMenu.style.display = "block"; //hýzlý eriþim açýlýr menüsünü aç.
		hizliErisim.style.borderBottom = "3px solid #FFF"; //hýzlý eriþim linki altýna border efekti ekle.
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
	if (suAnkiKonum >= headerUzunluk) { // þu anki konum header uzunluðundan büyük veya eþitse :
		header.classList.add("seffafHeader"); //header þeffaf olsun.
		logo.setAttribute("src", "img/logo.png"); //logo da þeffaflýða uygun biçimde deðiþsin.
		basaDonButonu.style.display = "block"; //baþa dön butonu gözüksün.
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

$(document).ready(function () {// sliderýn slide larý arasýna 20px padding uyguluyor
	$('.carousel').on('slide.bs.carousel', function () {
		$('.carousel .item').css('margin-right', '20px');
	});

	$('.carousel').on('slid.bs.carousel', function () {
		$('.carousel .item').css('margin-right', '0');
	});
});


window.addEventListener("scroll", seffafHeader); //scroll yapýldýkça seffafHeader fonksiyonu çaðrýlsýn.
window.addEventListener("scroll", dustatistikSayac); //scroll yapýldýkça dustatistikSayac fonksiyonu çaðrýlsýn.