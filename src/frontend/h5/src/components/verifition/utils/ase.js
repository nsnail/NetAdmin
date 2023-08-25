import CryptoJS from "crypto-js";

/**
 * @word 要加密的内容
 * @keyWord String  服务器随机返回的关键字
 *  */
export function aesEncrypt(word, keyWord = "1Z?f(2)%v?:X5NYRl+]PSi.rDf7Ip#lB") {
    const key = CryptoJS.enc.Utf8.parse(keyWord);
    const srcs = CryptoJS.enc.Utf8.parse(word);
    const encrypted = CryptoJS.AES.encrypt(srcs, key, {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7,
    });
    return encrypted.toString();
}