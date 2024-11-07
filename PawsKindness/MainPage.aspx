<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="PawsKindness.MainPage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Главная</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <p style="text-align: center;">Новостная лента</p>

  <div class="news-feed">

    <div class="news-item">
      <img src="c6321a9c846f089f89324b901b6d7753.jpg" alt="Подарки для приютов">
      <div class="news-content">
        <h4>Поддержите нас подарками!</h4>
        <p>В честь дня защиты животных мы добавили возможность отправлять приютам подарки. Помогите питомцам получить корм, игрушки и необходимые медицинские средства.
            <br /> _______________________________
        </p>
        <div class="news-date">06 ноября 2024</div>
      </div>
    </div>

    <div class="news-item">
      <img src="foster_program.jpg" alt="Программа опеки">
      <div class="news-content">
        <h4>Программа “Друзья для пушистиков”</h4>
        <p>Теперь вы можете стать временным опекуном питомца, пока он ждет своего постоянного хозяина. Сделайте доброе дело! <br /> _______________________________ </p>
        <div class="news-date">05 ноября 2024</div>
      </div>
    </div>

    <div class="news-item">
      <img src="t7AJyCbGynI.jpg" alt="Карточка питомца">
      <div class="news-content">
        <h4>Обновленные карточки питомцев</h4>
        <p>Мы добавили информацию о дне рождении каждого питомца. Узнайте, когда и кто родился!  _______________________________ </p>
        <div class="news-date">04 ноября 2024</div>
      </div>
    </div>

    <div class="news-item">
      <img src="f1ca0069bf01e23fd9bd9fe6cc8e81e6.jpg" alt="Истории успеха">
      <div class="news-content">
        <h4>Новая вкладка “Истории успеха”</h4>
        <p>Вдохновляйтесь историями людей, которые уже приютили питомцев и сделали их счастливыми. Эти истории обязательно поднимут настроение!
            <br /> _______________________________
        </p>
        <div class="news-date">03 ноября 2024</div>
      </div>
    </div>
  </div>
</asp:Content>
                      