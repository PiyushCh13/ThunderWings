mergeInto(LibraryManager.library, {
  DetectMobileDevice: function () {
    var ua = navigator.userAgent || navigator.vendor || window.opera;
    if (/android/i.test(ua)) {
      return true;
    }
    if (/iPad|iPhone|iPod/.test(ua) && !window.MSStream) {
      return true;
    }
    return false;
  }
});
