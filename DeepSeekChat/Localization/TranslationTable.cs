namespace DeepSeekChat.Localization;

/// <summary>
/// The UI translation table. 
/// The values in each row follow the order of <see cref="Languages"/>.
/// </summary>
internal static class TranslationTable
{
    /// <summary>
    /// Gets languages.
    /// </summary>
    public static IReadOnlyList<Language> Languages { get; } = new[]
    {
        new Language("en", "English"),
        new Language("ru", "Русский"),
        new Language("fr", "Français"),
        new Language("zh", "中文"),
        new Language("ja", "日本語"),
        new Language("hi", "हिन्दी"),
        new Language("es", "Español"),
        new Language("de", "Deutsch"),
        new Language("pt", "Português"),
        new Language("ko", "한국어"),
    };

    /// <summary>
    /// Gets rows.
    /// </summary>
    public static IReadOnlyDictionary<string, string[]> Rows { get; } = new Dictionary<string, string[]>
    {
        ["Back"] = new[] { "Back to chat", "Назад к чату", "Retour au chat", "返回聊天", "チャットに戻る", "चैट पर वापस", "Volver al chat", "Zurück zum Chat", "Voltar ao chat", "채팅으로 돌아가기" },
        ["NewChat"] = new[] { "New chat", "Новый чат", "Nouveau chat", "新建聊天", "新しいチャット", "नई चैट", "Nuevo chat", "Neuer Chat", "Novo chat", "새 채팅" },
        ["History"] = new[] { "Chat history", "История чатов", "Historique", "聊天记录", "チャット履歴", "चैट इतिहास", "Historial de chats", "Chatverlauf", "Histórico de chats", "채팅 기록" },
        ["DeleteChat"] = new[] { "Delete current chat", "Удалить текущий чат", "Supprimer ce chat", "删除当前聊天", "現在のチャットを削除", "वर्तमान चैट हटाएँ", "Eliminar chat actual", "Aktuellen Chat löschen", "Excluir chat atual", "현재 채팅 삭제" },
        ["Settings"] = new[] { "Settings", "Настройки", "Paramètres", "设置", "設定", "सेटिंग्स", "Ajustes", "Einstellungen", "Configurações", "설정" },
        ["Account"] = new[] { "Account", "Аккаунт", "Compte", "账户", "アカウント", "खाता", "Cuenta", "Konto", "Conta", "계정" },
        ["Copy"] = new[] { "Copy", "Копировать", "Copier", "复制", "コピー", "कॉपी करें", "Copiar", "Kopieren", "Copiar", "복사" },
        ["ChangeKey"] = new[] { "Change", "Изменить", "Modifier", "更改", "変更", "बदलें", "Cambiar", "Ändern", "Alterar", "변경" },
        ["Attach"] = new[] { "Attach", "Прикрепить", "Joindre", "添加附件", "添付", "संलग्न करें", "Adjuntar", "Anhängen", "Anexar", "첨부" },
        ["AddPhoto"] = new[] { "Add photo", "Добавить фото", "Ajouter une photo", "添加照片", "写真を追加", "फ़ोटो जोड़ें", "Añadir foto", "Foto hinzufügen", "Adicionar foto", "사진 추가" },
        ["AddFile"] = new[] { "Add file", "Добавить файл", "Ajouter un fichier", "添加文件", "ファイルを追加", "फ़ाइल जोड़ें", "Añadir archivo", "Datei hinzufügen", "Adicionar arquivo", "파일 추가" },
        ["WelcomeTitle"] = new[] { "Hello! 🎉", "Привет! 🎉", "Bonjour ! 🎉", "你好！🎉", "こんにちは！🎉", "नमस्ते! 🎉", "¡Hola! 🎉", "Hallo! 🎉", "Olá! 🎉", "안녕하세요! 🎉" },
        ["WelcomeSub"] = new[] { "I am your AI assistant in Visual Studio.", "Я ваш ИИ-помощник в Visual Studio.", "Je suis votre assistant IA dans Visual Studio.", "我是你在 Visual Studio 中的 AI 助手。", "私は Visual Studio のAIアシスタントです。", "मैं Visual Studio में आपका AI सहायक हूँ।", "Soy tu asistente de IA en Visual Studio.", "Ich bin dein KI-Assistent in Visual Studio.", "Sou seu assistente de IA no Visual Studio.", "저는 Visual Studio의 AI 어시스턴트입니다." },
        ["WelcomeDesc"] = new[] { "I can help with code, explain errors, suggest improvements, and much more.", "Помогу с кодом, объясню ошибки, предложу улучшения и многое другое.", "Je peux aider avec le code, expliquer les erreurs, suggérer des améliorations et bien plus.", "我可以帮助处理代码、解释错误、提出改进建议等。", "コードの支援、エラーの説明、改善の提案など、さまざまなことをお手伝いします。", "मैं कोड में मदद, त्रुटियों की व्याख्या, सुधार के सुझाव और बहुत कुछ कर सकता हूँ।", "Puedo ayudar con el código, explicar errores, sugerir mejoras y mucho más.", "Ich helfe bei Code, erkläre Fehler, schlage Verbesserungen vor und vieles mehr.", "Posso ajudar com código, explicar erros, sugerir melhorias e muito mais.", "코드 작성, 오류 설명, 개선 제안 등 다양한 도움을 드릴 수 있습니다." },
        ["QuickExplain"] = new[] { "Explain this code", "Объясни этот код", "Explique ce code", "解释这段代码", "このコードを説明して", "इस कोड को समझाएँ", "Explica este código", "Erkläre diesen Code", "Explique este código", "이 코드를 설명해 줘" },
        ["QuickFix"] = new[] { "Fix the error", "Исправь ошибку", "Corrige l'erreur", "修复错误", "エラーを修正して", "त्रुटि ठीक करें", "Corrige el error", "Behebe den Fehler", "Corrija o erro", "오류를 수정해 줘" },
        ["QuickTests"] = new[] { "Write tests", "Напиши тесты", "Écris des tests", "编写测试", "テストを書いて", "टेस्ट लिखें", "Escribe pruebas", "Schreibe Tests", "Escreva testes", "테스트를 작성해 줘" },
        ["QuickOptimize"] = new[] { "Optimize performance", "Оптимизируй производительность", "Optimise les performances", "优化性能", "パフォーマンスを最適化して", "प्रदर्शन अनुकूलित करें", "Optimiza el rendimiento", "Optimiere die Leistung", "Otimize o desempenho", "성능을 최적화해 줘" },
        ["Typing"] = new[] { "Typing...", "Печатает...", "Écrit...", "正在输入...", "入力中...", "टाइप कर रहा है...", "Escribiendo...", "Schreibt...", "Digitando...", "입력 중..." },
        ["Placeholder"] = new[] { "Type a message...", "Введите сообщение...", "Écrivez un message...", "输入消息...", "メッセージを入力...", "संदेश लिखें...", "Escribe un mensaje...", "Nachricht eingeben...", "Digite uma mensagem...", "메시지를 입력하세요..." },
        ["HistoryTitle"] = new[] { "Chat History", "История чатов", "Historique des chats", "聊天记录", "チャット履歴", "चैट इतिहास", "Historial de chats", "Chatverlauf", "Histórico de chats", "채팅 기록" },
        ["HistoryEmpty"] = new[] { "No saved chats yet.", "Сохранённых чатов пока нет.", "Aucun chat enregistré.", "还没有保存的聊天。", "保存されたチャットはまだありません。", "अभी तक कोई सहेजी गई चैट नहीं।", "Aún no hay chats guardados.", "Noch keine gespeicherten Chats.", "Nenhum chat salvo ainda.", "저장된 채팅이 없습니다." },
        ["SettingsTitle"] = new[] { "DeepSeek Settings", "Настройки DeepSeek", "Paramètres DeepSeek", "DeepSeek 设置", "DeepSeek 設定", "DeepSeek सेटिंग्स", "Ajustes de DeepSeek", "DeepSeek-Einstellungen", "Configurações do DeepSeek", "DeepSeek 설정" },
        ["ApiKeyLabel"] = new[] { "DeepSeek API key", "API-ключ DeepSeek", "Clé API DeepSeek", "DeepSeek API 密钥", "DeepSeek APIキー", "DeepSeek API कुंजी", "Clave API de DeepSeek", "DeepSeek-API-Schlüssel", "Chave de API do DeepSeek", "DeepSeek API 키" },
        ["SettingsInfo"] = new[] { "The key is stored locally on the computer and is not saved in the source code.", "Ключ хранится локально на компьютере и не сохраняется в исходном коде.", "La clé est stockée localement sur l'ordinateur et n'est pas enregistrée dans le code source.", "密钥仅保存在本机，不会写入源代码。", "キーはこのコンピューターにのみ保存され、ソースコードには保存されません。", "कुंजी केवल इस कंप्यूटर पर संग्रहीत होती है और सोर्स कोड में सहेजी नहीं जाती।", "La clave se guarda localmente en el equipo y no en el código fuente.", "Der Schlüssel wird lokal auf dem Computer gespeichert, nicht im Quellcode.", "A chave é armazenada localmente no computador e não no código-fonte.", "키는 이 컴퓨터에만 저장되며 소스 코드에는 저장되지 않습니다." },
        ["Save"] = new[] { "Save", "Сохранить", "Enregistrer", "保存", "保存", "सहेजें", "Guardar", "Speichern", "Salvar", "저장" },
        ["LanguageLabel"] = new[] { "Language", "Язык", "Langue", "语言", "言語", "भाषा", "Idioma", "Sprache", "Idioma", "언어" },
        ["ThemeLabel"] = new[] { "Theme", "Тема", "Thème", "主题", "テーマ", "थीम", "Tema", "Design", "Tema", "테마" },
        ["AccountTitle"] = new[] { "DeepSeek Account", "Аккаунт DeepSeek", "Compte DeepSeek", "DeepSeek 账户", "DeepSeek アカウント", "DeepSeek खाता", "Cuenta de DeepSeek", "DeepSeek-Konto", "Conta do DeepSeek", "DeepSeek 계정" },
        ["ApiKeyField"] = new[] { "API key:", "API-ключ:", "Clé API :", "API 密钥：", "APIキー：", "API कुंजी:", "Clave API:", "API-Schlüssel:", "Chave de API:", "API 키:" },
        ["BalanceField"] = new[] { "Balance:", "Баланс:", "Solde :", "余额：", "残高：", "शेष राशि:", "Saldo:", "Guthaben:", "Saldo:", "잔액:" },
        ["StatusField"] = new[] { "Status:", "Статус:", "Statut :", "状态：", "状態：", "स्थिति:", "Estado:", "Status:", "Status:", "상태:" },
        ["NameLabel"] = new[] { "Your name", "Ваше имя", "Votre nom", "你的名字", "お名前", "आपका नाम", "Tu nombre", "Dein Name", "Seu nome", "이름" },
        ["NameHint"] = new[] { "DeepSeek will address you by this name.", "DeepSeek будет обращаться к вам по этому имени.", "DeepSeek vous appellera par ce nom.", "DeepSeek 会用这个名字称呼你。", "DeepSeek はこの名前であなたを呼びます。", "DeepSeek आपको इसी नाम से संबोधित करेगा।", "DeepSeek se dirigirá a ti con este nombre.", "DeepSeek spricht dich mit diesem Namen an.", "O DeepSeek vai chamar você por este nome.", "DeepSeek이 이 이름으로 당신을 부릅니다." },
        ["ModelLabel"] = new[] { "Model", "Модель", "Modèle", "模型", "モデル", "मॉडल", "Modelo", "Modell", "Modelo", "모델" },
        ["NameSaved"] = new[] { "Name saved", "Имя сохранено", "Nom enregistré", "名字已保存", "名前を保存しました", "नाम सहेजा गया", "Nombre guardado", "Name gespeichert", "Nome salvo", "이름이 저장되었습니다" },
        ["ApiKeySaved"] = new[] { "API key saved", "Ключ API сохранён", "Clé API enregistrée", "API 密钥已保存", "APIキーを保存しました", "API कुंजी सहेजी गई", "Clave API guardada", "API-Schlüssel gespeichert", "Chave de API salva", "API 키가 저장되었습니다" },
        ["ApiKeyEmpty"] = new[] { "API key is not specified.", "API-ключ не указан.", "La clé API n'est pas renseignée.", "未填写 API 密钥。", "APIキーが指定されていません。", "API कुंजी दर्ज नहीं की गई।", "No se ha indicado la clave API.", "Es wurde kein API-Schlüssel angegeben.", "A chave de API não foi informada.", "API 키가 지정되지 않았습니다." },
        ["ApiKeyInvalid"] = new[] { "The API key is invalid.\n\n{0}", "Ключ API недействителен.\n\n{0}", "La clé API est invalide.\n\n{0}", "API 密钥无效。\n\n{0}", "APIキーが無効です。\n\n{0}", "API कुंजी अमान्य है।\n\n{0}", "La clave API no es válida.\n\n{0}", "Der API-Schlüssel ist ungültig.\n\n{0}", "A chave de API é inválida.\n\n{0}", "API 키가 올바르지 않습니다.\n\n{0}" },
        ["ConfirmDeleteCurrent"] = new[] { "Delete the current chat?\n\nIt will be permanently removed from history.", "Удалить текущий чат?\n\nОн будет навсегда удалён из истории.", "Supprimer ce chat ?\n\nIl sera définitivement retiré de l'historique.", "删除当前聊天？\n\n它将从历史记录中永久删除。", "現在のチャットを削除しますか？\n\n履歴から完全に削除されます。", "वर्तमान चैट हटाएँ?\n\nइसे इतिहास से स्थायी रूप से हटा दिया जाएगा।", "¿Eliminar el chat actual?\n\nSe eliminará permanentemente del historial.", "Aktuellen Chat löschen?\n\nEr wird endgültig aus dem Verlauf entfernt.", "Excluir o chat atual?\n\nEle será removido permanentemente do histórico.", "현재 채팅을 삭제할까요?\n\n기록에서 영구적으로 삭제됩니다." },
        ["ConfirmDeleteChat"] = new[] { "Delete chat \"{0}\"?", "Удалить чат «{0}»?", "Supprimer le chat « {0} » ?", "删除聊天“{0}”？", "チャット「{0}」を削除しますか？", "चैट \"{0}\" हटाएँ?", "¿Eliminar el chat \"{0}\"?", "Chat „{0}“ löschen?", "Excluir o chat \"{0}\"?", "채팅 \"{0}\"을(를) 삭제할까요?" },
        ["SelectCodeFirst"] = new[] { "Select some code in the editor first.", "Сначала выделите код в редакторе.", "Sélectionnez d'abord du code dans l'éditeur.", "请先在编辑器中选择一些代码。", "先にエディターでコードを選択してください。", "पहले एडिटर में कुछ कोड चुनें।", "Primero selecciona código en el editor.", "Markiere zuerst Code im Editor.", "Selecione primeiro algum código no editor.", "먼저 편집기에서 코드를 선택하세요." },
        ["ErrorApiResponse"] = new[] { "Failed to get a response from DeepSeek:\n\n{0}", "Не удалось получить ответ от DeepSeek:\n\n{0}", "Impossible d'obtenir une réponse de DeepSeek :\n\n{0}", "无法从 DeepSeek 获取响应：\n\n{0}", "DeepSeek から応答を取得できませんでした：\n\n{0}", "DeepSeek से उत्तर प्राप्त नहीं हो सका:\n\n{0}", "No se pudo obtener respuesta de DeepSeek:\n\n{0}", "Keine Antwort von DeepSeek erhalten:\n\n{0}", "Falha ao obter resposta do DeepSeek:\n\n{0}", "DeepSeek에서 응답을 받지 못했습니다:\n\n{0}" },
        ["SenderYou"] = new[] { "You", "Вы", "Vous", "你", "あなた", "आप", "Tú", "Du", "Você", "나" },
        ["Error"] = new[] { "Error", "Ошибка", "Erreur", "错误", "エラー", "त्रुटि", "Error", "Fehler", "Erro", "오류" },
        ["FileReadError"] = new[] { "Could not read file: {0}", "Не удалось прочитать файл: {0}", "Impossible de lire le fichier : {0}", "无法读取文件：{0}", "ファイルを読み込めませんでした：{0}", "फ़ाइल पढ़ी नहीं जा सकी: {0}", "No se pudo leer el archivo: {0}", "Datei konnte nicht gelesen werden: {0}", "Não foi possível ler o arquivo: {0}", "파일을 읽을 수 없습니다: {0}" },
        ["FileTooLarge"] = new[] { "File is too large: {0}", "Файл слишком большой: {0}", "Fichier trop volumineux : {0}", "文件过大：{0}", "ファイルが大きすぎます：{0}", "फ़ाइल बहुत बड़ी है: {0}", "El archivo es demasiado grande: {0}", "Datei ist zu groß: {0}", "O arquivo é muito grande: {0}", "파일이 너무 큽니다: {0}" },
        ["KeyMissing"] = new[] { "Missing", "Отсутствует", "Absente", "缺失", "未設定", "अनुपलब्ध", "Falta", "Fehlt", "Ausente", "없음" },
        ["KeyInvalid"] = new[] { "Invalid", "Недействителен", "Invalide", "无效", "無効", "अमान्य", "No válida", "Ungültig", "Inválida", "유효하지 않음" },
        ["KeyValid"] = new[] { "Valid", "Действителен", "Valide", "有效", "有効", "मान्य", "Válida", "Gültig", "Válida", "유효함" },
        ["KeyCheckFailed"] = new[] { "Check failed", "Ошибка проверки", "Échec de la vérification", "检查失败", "確認に失敗", "जाँच विफल", "Error de comprobación", "Prüfung fehlgeschlagen", "Falha na verificação", "확인 실패" },
        ["BalanceUnknown"] = new[] { "Unknown", "Неизвестно", "Inconnu", "未知", "不明", "अज्ञात", "Desconocido", "Unbekannt", "Desconhecido", "알 수 없음" },
        ["StatusActive"] = new[] { "Active", "Активен", "Actif", "正常", "有効", "सक्रिय", "Activa", "Aktiv", "Ativa", "활성" },
        ["StatusInsufficient"] = new[] { "Insufficient funds", "Недостаточно средств", "Solde insuffisant", "余额不足", "残高不足", "अपर्याप्त शेष", "Fondos insuficientes", "Guthaben nicht ausreichend", "Saldo insuficiente", "잔액 부족" },
        ["StatusNoKey"] = new[] { "No API key", "Нет API-ключа", "Pas de clé API", "无 API 密钥", "APIキーなし", "कोई API कुंजी नहीं", "Sin clave API", "Kein API-Schlüssel", "Sem chave de API", "API 키 없음" },
        ["StatusNoConnection"] = new[] { "No connection", "Нет соединения", "Pas de connexion", "无连接", "接続なし", "कोई कनेक्शन नहीं", "Sin conexión", "Keine Verbindung", "Sem conexão", "연결 없음" },
        ["ExplainTemplate"] = new[] { "Explain this code:\n\n{0}", "Объясни этот код:\n\n{0}", "Explique ce code :\n\n{0}", "请解释这段代码：\n\n{0}", "このコードを説明してください：\n\n{0}", "इस कोड को समझाएँ:\n\n{0}", "Explica este código:\n\n{0}", "Erkläre diesen Code:\n\n{0}", "Explique este código:\n\n{0}", "이 코드를 설명해 주세요:\n\n{0}" },
        ["RefactorTemplate"] = new[] { "Refactor this code:\n\n{0}", "Отрефактори этот код:\n\n{0}", "Refactorise ce code :\n\n{0}", "请重构这段代码：\n\n{0}", "このコードをリファクタリングしてください：\n\n{0}", "इस कोड को रीफ़ैक्टर करें:\n\n{0}", "Refactoriza este código:\n\n{0}", "Refaktoriere diesen Code:\n\n{0}", "Refatore este código:\n\n{0}", "이 코드를 리팩터링해 주세요:\n\n{0}" },
        ["ThemeViolet"] = new[] { "Violet", "Фиолетовая", "Violet", "紫罗兰", "バイオレット", "वायलेट", "Violeta", "Violett", "Violeta", "바이올렛" },
        ["ThemeLight"] = new[] { "Light", "Светлая", "Clair", "浅色", "ライト", "लाइट", "Claro", "Hell", "Claro", "라이트" },
        ["ThemeGraphite"] = new[] { "Graphite", "Графит", "Graphite", "石墨", "グラファイト", "ग्रेफाइट", "Grafito", "Graphit", "Grafite", "그래파이트" },
        ["ThemeOcean"] = new[] { "Ocean", "Океан", "Océan", "海洋", "オーシャン", "ओशन", "Océano", "Ozean", "Oceano", "오션" },
        ["ThemePlum"] = new[] { "Plum", "Слива", "Prune", "李子", "プラム", "प्लम", "Ciruela", "Pflaume", "Ameixa", "플럼" },
        ["ThemeForest"] = new[] { "Forest", "Лес", "Forêt", "森林", "フォレスト", "फ़ॉरेस्ट", "Bosque", "Wald", "Floresta", "숲" },
    };
}
