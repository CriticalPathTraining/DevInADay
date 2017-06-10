/// <reference path="jquery-3.1.1.js" />
/// <reference path="powerbi.js" />

var leftNavCollapsed = window.leftNavCollapsed;

$(InitializeUI);

function InitializeUI() {

  $("#navigation-toggle").click(onNavigationToggle);

  $("#reportsMenu a").click(onRequestReport);
  
  $("#report-elements").hide();

  $(".leftNavIcon").click(onNavigationToggle);
  $("#print-report-page").click(printReportPage);
    

  setNavigation();

  $(window).resize(resizeNavigationPane);
  resizeNavigationPane();

}

function resizeNavigationPane() {
  var windowHeight = $(window).height();
  var bannerHeight = $("#banner").height();
  $("#left-nav").height(windowHeight - bannerHeight);
}

function onNavigationToggle() {
  leftNavCollapsed = !leftNavCollapsed;
  setNavigation();

}

function setNavigation() {
  if (leftNavCollapsed) {
    console.log("Collpase");
    $("#left-nav").addClass("navigationPaneCollapsed");
    $("#content-body").addClass("contentBodyNavigationCollapsed");
    $("#navigation-toggle").addClass("fa-arrow-left").removeClass("fa-mail-reply");
    $(".leftNavItem").addClass("leftNavHide").hide();
  }
  else {
    console.log("Expand");
    $("#left-nav").removeClass("navigationPaneCollapsed");
    $("#content-body").removeClass("contentBodyNavigationCollapsed");
    $("#navigation-toggle").addClass("fa-mail-reply").toggleClass("fa-arrow-left");
    $(".leftNavItem").removeClass("leftNavHide").show();
  }
}

function onRequestReport() {
  $("#report-elements").hide();
  $("#report-loading-message").show();
}

function loadReportPages(pages) {

  var pageNavigation = $("#page-navigation");
  pageNavigation.empty();
  for (var index = 0; index < pages.length; index++) {
    pageNavigation.append(
      $("<li>")
        .append($('<a href="javascript:;" >')
        .text(pages[index].displayName))
        .click(function (e) {
          pages.find(function (page) { return page.displayName === e.target.text }).setActive();
        }));


    $("#report-loading-message").hide();
    $("#report-elements").show();
  }
}

function setActiveReportPage(page) {
  // update page name in title area
  $("#reportTitleInTitleArea").html("&nbsp; > &nbsp;" + page.displayName);
  // update page links to reflect which page is active
  var pageLinks = $("#page-navigation a");
  pageLinks.each(function (index, value) {
    if ($(this).text() === page.displayName) {
      $(this).parent().addClass("activePage");
    }
    else {
      $(this).parent().removeClass("activePage");
    }
  });
}

function printReportPage() {
  window.print();
}