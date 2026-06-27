# UnityCN EasyTool

<div align="center">
  <img src="https://img.shields.io/badge/Platform-Windows-blue" alt="Platform">
  <img src="https://img.shields.io/badge/Framework-WPF%20%7C%20.NET%208-purple" alt="Framework">
  <img src="https://img.shields.io/badge/Language-C%23-green" alt="Language">
</div>

---

## 🇹🇭 ภาษาไทย

**UnityCN EasyTool** เป็นโปรแกรมที่มีหน้าต่างผู้ใช้งาน (GUI) ที่ถูกพัฒนาขึ้นมาเพื่อให้การเข้ารหัสและถอดรหัสไฟล์ AssetBundle ของเกมที่ใช้เอนจิน Unity (โดยเฉพาะฝั่งเซิร์ฟเวอร์จีน) ทำได้ง่ายและสะดวกมากยิ่งขึ้น โดยไม่ต้องพิมพ์คำสั่งผ่าน Command Line ให้ยุ่งยากอีกต่อไป

โปรเจกต์นี้เป็นการนำเครื่องมือแบบ Command Line ชื่อ [UnityCN-Helper](https://github.com/AXiX-official/UnityCN-Helper) (พัฒนาโดย AXiX-official) มาต่อยอดสร้างหน้าต่างกราฟิก (GUI) สไตล์ Material Design และทำเป็นโปรแกรมแบบ Self-Contained ที่สามารถเปิดใช้งานได้ทันทีโดยไม่ต้องติดตั้ง .NET Runtime ใดๆ เพิ่มเติม

### ✨ คุณสมบัติหลัก
* **ลากแล้ววาง (Drag & Drop):** รองรับการลากไฟล์ `.bundle` หรือแม้แต่การลาก "โฟลเดอร์" เข้ามาใส่ในโปรแกรมได้โดยตรง
* **โพรไฟล์เกมพร้อมใช้:** มีการเตรียมรายชื่อเกมและ Decryption Key ที่ใช้บ่อยไว้ล่วงหน้า (เช่น Punishing: Gray Raven)
* **โหมดการทำงานครบครัน:** รองรับทั้งการถอดรหัส (Decrypt) และการเข้ารหัสกลับ (Encrypt)
* **สวยงามและทันสมัย:** หน้าต่างโปรแกรมใช้เทคโนโลยี WPF และ Material Design Themes (Dark Mode)

### 🛠️ วิธีใช้งาน
1. **เลือกเกม (Select Game):** เลือกชื่อเกมจากเมนู Dropdown (หรือพิมพ์ Key ของเกมนั้นด้วยตนเองในช่อง `Key`)
2. **เลือกโหมด (Mode):** เลือกว่าต้องการ "ถอดรหัส (Decrypt)" หรือ "เข้ารหัส (Encrypt)"
3. **ใส่ไฟล์:** กดปุ่ม `Browse...` เพื่อเลือกไฟล์/โฟลเดอร์ หรือ **ลากไฟล์/โฟลเดอร์มาวาง** ตรงกลางหน้าต่างโปรแกรม
4. **ตั้งค่า Output:** เลือกว่าจะบันทึกไฟล์ผลลัพธ์ไว้ที่ "โฟลเดอร์เดิม (Same Folder)" หรือระบุ "โฟลเดอร์ใหม่ (Custom)"
5. **เริ่มทำงาน:** กดปุ่ม `🚀 START ENGINE` แล้วรอจนแถบสถานะ (Progress Bar) เต็ม 100% (สามารถดูรายละเอียดการทำงานได้ในหน้าต่าง Console ด้านล่าง)

---

## 🇨🇳 中文

**UnityCN EasyTool** 是一个带有图形用户界面（GUI）的程序，旨在使中国服务器 Unity 游戏的 AssetBundle 文件的加密和解密变得更加简单和方便，无需再繁琐地输入命令行。

该项目基于 [UnityCN-Helper](https://github.com/AXiX-official/UnityCN-Helper) (由 AXiX-official 开发) 的命令行工具，为其添加了 Material Design 风格的图形界面，并打包成无需安装任何额外 .NET 运行环境即可直接运行的独立 (Self-Contained) 程序。

### ✨ 主要功能
* **拖放支持 (Drag & Drop):** 支持直接将 `.bundle` 文件或整个“文件夹”拖放到程序窗口中。
* **预设游戏配置文件:** 预先准备了常用的游戏名称及其解密密钥 (Decryption Key)，例如《战双帕弥什》(Punishing: Gray Raven)。
* **完整的操作模式:** 同时支持文件解密 (Decrypt) 和加密 (Encrypt)。
* **美观现代的界面:** 使用 WPF 技术和 Material Design Themes (暗色模式) 打造。

### 🛠️ 使用方法
1. **选择游戏 (Select Game):** 从下拉菜单中选择游戏名称（或在 `Key` 框中手动输入该游戏的解密密钥）。
2. **选择模式 (Mode):** 选择您需要 “解密 (Decrypt)” 还是 “加密 (Encrypt)”。
3. **导入文件:** 点击 `Browse...` 按钮选择文件/文件夹，或者 **直接将文件/文件夹拖拽** 到程序窗口中央。
4. **设置输出路径:** 选择将处理后的文件保存在 “原文件夹 (Same Folder)”，还是指定一个 “自定义文件夹 (Custom)”。
5. **开始执行:** 点击 `🚀 START ENGINE` 按钮，等待进度条满 100%（您可以在下方的控制台窗口中查看详细的运行日志）。
