function UploadFileCheck(source, arguments) {
    var sFile = arguments.Value;
    arguments.IsValid =
       ((sFile.endsWith('.jpg')) ||
        (sFile.endsWith('.jpeg')) ||
        (sFile.endsWith('.png')));
}