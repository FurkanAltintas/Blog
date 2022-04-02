function convertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
}

// true
// t => T
// T + rue => True

function convertToShartDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}

/*
 * Dil ve Ülke Formatları
 * 
 * Almanya (Almanca) -> de-DE
 * Birleşik Krallık (İngilizce) -> en-GB
 * Çin (Çince) -> zh-CN
 * Fransa (Fransızca) -> fr-FR
 * Hindistan (Hintçe) -> hi-IN
 * Rusya (Rusça) -> ru-RU
 * Türkiye (Türkçe) -> tr-TR
*/