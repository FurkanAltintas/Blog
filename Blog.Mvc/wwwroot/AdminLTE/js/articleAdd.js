$(document).ready(function () {

    // Trumbowyg Start
    $('#text-editor').trumbowyg({
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize']
        ]
    });
    // Trumbowyg End


    // Select2 Start
    $('#categoryList').select2({
        placeholder: "Lütfen bir kategori seçiniz",
        allowClear: true,
        theme: 'bootstrap4'
    });
    // Select2 End


    // DatePicker Start
    $("#datepicker").datepicker({
        closeText: 'kapat',
        prevText: '&#x3C;geri',
        nextText: 'ileri&#x3e',
        currentText: 'bugün',
        monthNames: ['Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran',
            'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'],
        monthNamesShort: ['Oca', 'Şub', 'Mar', 'Nis', 'May', 'Haz',
            'Tem', 'Ağu', 'Eyl', 'Eki', 'Kas', 'Ara'],
        dayNames: ['Pazar', 'Pazartesi', 'Salı', 'Çarşamba', 'Perşembe', 'Cuma', 'Cumartesi'],
        dayNamesShort: ['Pz', 'Pt', 'Sa', 'Ça', 'Pe', 'Cu', 'Ct'],
        dayNamesMin: ['Pz', 'Pt', 'Sa', 'Ça', 'Pe', 'Cu', 'Ct'],
        weekHeader: 'Hf',
        dateFormat: 'dd.mm.yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: '',
        duration: 1000,
        showAnim: "drop",
        showOptions: { direction: "down"},
        minDate: -3, // Şuan ki tarihten 3 gün öncesine kadar izin vericek,
        maxDate: +3 // 3 gün sonrasına kadar seçim yapılmasına izin verilecek
    });
    // DatePicker End

});