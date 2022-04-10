$(document).ready(function () {

    /* DataTable */

    const dataTable = $('#categoriesTable').DataTable({
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
                        url: '/Admin/Category/GetAllCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            dataTable.clear();
                            if (categoryListDto.ResultStatus === 0) {
                                $.each(categoryListDto.Categories.$values, function (index, category) {
                                    const newTableRow = dataTable.row.add([
                                        category.Id,
                                        category.Name,
                                        category.Description,
                                        category.IsActive ? "True" : "False",
                                        category.IsDelete ? "True" : "False",
                                        convertToShartDate(category.CreatedDate),
                                        category.CreatedByName,
                                        convertToShartDate(category.ModifiedDate),
                                        category.ModifiedByName,
                                        category.Note,
                                        `
                                            <button class="btn btn-primary btn-sm btn-update" data-id="${category.Id}"><span class="fas fa-edit"></span></button>
                                            <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span></button>
                                        `
                                    ]).node(); // node fonksiyonu ile seçiyoruz.
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${category.Id}`);
                                });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
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

    /* Create */

    $(function () {
        const url = '/Admin/Category/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () { // ekle butona basılırsa ajax işlemi başattık
            $.get(url).done(function (data) {
                placeHolderDiv.html(data); // placeHolderDiv içerisine yazmış olduk
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-category-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const categoryAddAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                            <tr name="${categoryAddAjaxModel.CategoryDto.Category.Id}">
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Id}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                <td>${convertToShartDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                <td>${convertToShartDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
                                <td>
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-edit"></span></button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span></button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    newTableRowObject.hide();
                    $('#categoriesTable').append(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!');
                } else {
                    let summaryText = '';
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText += `*${text}\n`;
                    });
                    toastr.warning(summaryText);
                }
            });
        });
    });

    /* Delete */

    $(document).on('click', '.btn-delete', function () {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const categoryName = tableRow.find('td:eq(1)').text(); // 2. sıradaki table datayı seçtik.
        Swal.fire({
            title: 'Silmek istediğinize emin misiniz?',
            text: `${categoryName} adlı kategori silinicektir!`,
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
                    data: { categoryId: id },
                    url: '/Admin/Category/Delete/',
                    success: function (data) {
                        const categoryDto = jQuery.parseJSON(data);
                        if (categoryDto.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi!',
                                `${categoryName} adlı kategori başarıyla silinmiştir.`,
                                'success'
                            );
                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Başarısız İşlem!',
                                text: `${categoryDto.Message}`
                            });
                        }
                    },
                    error: function (err) {
                        toastr.error(`${err.responseText}`, "Hata!");
                    }
                });
            }
        });
    });

    /* Update */

    $(function () {
        const url = '/Admin/Category/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { categoryId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error('Bir hata oluştu.');
            });
        });
        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-category-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                            <tr name="${categoryUpdateAjaxModel.CategoryDto.Category.Id}">
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Id}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Name}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Description}</td>
                                <td>${convertFirstLetterToUpperCase(categoryUpdateAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                <td>${convertFirstLetterToUpperCase(categoryUpdateAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                <td>${convertToShartDate(categoryUpdateAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                <td>${convertToShartDate(categoryUpdateAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Note}</td>
                                <td>
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-edit"></span></button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span></button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"]`);
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${categoryUpdateAjaxModel.CategoryDto.Message}`, "Başarılı İşlem!");
                } else {
                    let summaryText = '';
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText += `*${text}\n`;
                    });
                    toastr.warning(summaryText);
                }
            }).fail(function (response) {

            });
        });

    });
});