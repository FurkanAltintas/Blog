$(document).ready(function () {

    /* DataTable */

    const dataTable = $('#articlesTable').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: 'btnAdd',
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Article/GetAllArticles/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#articlesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const articleResult = jQuery.parseJSON(data);
                            dataTable.clear();
                            if (articleResult.Data.ResultStatus === 0) {
                                let categoriesArray = [];
                                $.each(articleResult.Data.Articles.$values, function (index, article) {
                                    const newArticle = getJsonNetObject(article, articleResult.Data.Articles.$values);
                                    let newCategory = getJsonNetObject(newArticle.Category, newArticle);
                                    if (newCategory !== null) {
                                        categoriesArray.push(newCategory);
                                    }
                                    if (newCategory === null) {
                                        newCategory = categoriesArray.find((category) => {
                                            return category.$id === newArticle.Category.$ref;
                                        });
                                    }
                                    const newTableRow = dataTable.row.add([
                                        newArticle.Id,
                                        newCategory.Name,
                                        newArticle.Title,
                                        `<img src="/img/${newArticle.Thumbnail}" alt="${newArticle.Title}" class="my-image-table" />`,
                                        `${convertToShortDate(newArticle.Date)}`,
                                        newArticle.ViewCount,
                                        newArticle.CommentCount,
                                        `${newArticle.IsActive ? "Evet" : "Hayır"}`,
                                        `${newArticle.IsDeleted ? "Evet" : "Hayır"}`,
                                        `${convertToShortDate(newArticle.CreatedDate)}`,
                                        newArticle.CreatedByName,
                                        `${convertToShortDate(newArticle.ModifiedDate)}`,
                                        newArticle.ModifiedByName,
                                        `
                                            <a class="btn btn-primary btn-sm btn-update" href="/Admin/Article/Update?articleId=${newArticle.Id}"><span class="fas fa-edit"></span></a>
                                            <button class="btn btn-danger btn-sm btn-delete" data-id="${newArticle.Id}"><span class="fas fa-minus-circle"></span></button>
                                        `
                                    ]).node(); // node fonksiyonu ile seçiyoruz.
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${newArticle.Id}`);
                                });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#articlesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${articleResult.Data.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (error) {
                            $('.spinner-border').hide();
                            $('#articlesTable').fadeIn(1000);
                            toastr.error(`${error.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi"
                },
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            }
        }
    });

    /* Delete */

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const articleTitle = tableRow.find('td:eq(2)').text(); // 3. sıradaki table datayı seçtik.
        Swal.fire({
            title: 'Silmek istediğinize emin misiniz?',
            text: `${articleTitle} başlıklı makale silinicektir!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, silmek istiyorum.',
            cancelButtonText: 'Hayır, silmek istemiyorum.'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { articleId: id },
                    url: '/Admin/Article/Delete/',
                    success: function (data) {
                        const articleResult = jQuery.parseJSON(data);
                        if (articleResult.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi!',
                                `${articleResult.Message}`,
                                'success'
                            );
                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Başarısız İşlem!',
                                text: `${articleResult.Message}`
                            });
                        }
                    },
                    error: function (error) {
                        toastr.error(`${error.responseText}`, "Hata!");
                    }
                });
            }
        });
    });
});