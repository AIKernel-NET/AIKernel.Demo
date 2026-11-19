# DM2: READMEとWeb API実装の整合

対象はDemoのDM2のみです。`dev` (`b3fcb62`) 起点の`review/DM2`で作業しています。
アプリ本体、公開契約、パッケージ参照、バージョン、ハッシュ計算、既存テストは変更していません。

## 確認した不整合

日英READMEは`/v1/chat/completions`、ProviderRouterによる選択、PDP判定、ReplayLogの
HTTP応答ヘッダーを実装済みとして案内していました。ユーザーガイドと構成説明にも同じ案内がありました。
実際の`src/AIKernel.Demo.WebApi/Program.cs`は`GET /`と`GET /demo/pipeline`だけを登録し、
`DemoPipelineCatalog.CreateDefaultRun()`の固定データを返します。

## 変更

- 日英READMEのプロジェクト説明を修正し、起動・取得・停止の手順、ルートとステータス、JSON応答例を追加しました。
- `GET /`の302リダイレクト、`GET /demo/pipeline`の200、未実装の`POST /v1/chat/completions`の404を明記しました。
- 数値の`decision: 0`はサンプルのAllowであり、要求ごとのポリシー判定ではないことを説明しました。
  `replay`もJSON本文中の固定要約で、HTTPヘッダーではありません。モデル推論やプロバイダー呼出しは行いません。
- 日英ユーザーガイドと構成説明を同じ内容に揃え、詳しいAPI案内へリンクしました。
- OpenAI互換ゲートウェイの実装・仕様はDM4のままです。今回APIの動作を追加・変更していません。

## 追加検証

`tests/webapi_docs_smoke.py`はPython標準ライブラリのunittestで、ビルド済みのWebApiを
ループバックの空きポートで起動し、終了時には起動したプロセスだけを停止します。
外部サービス、認証情報、モデル、GPUは使用しません。

4件のテストで、日英READMEのリクエスト表6行と実HTTPステータス、JSON例2つと応答本文の
項目・値・型、リダイレクト先、replay要約が本文だけにあることを検証します。
文書に書かれたステータスとJSONを読み取って実ホストへ照合し、ソースコードの文字列一致では判定しません。

```powershell
dotnet build AIKernel.Demo.slnx -c Release
dotnet test AIKernel.Demo.slnx -c Release --no-build
python tests/webapi_docs_smoke.py
```

## 検証結果

検証完了日: 2026-11-14（Windows）。DemoとDoomのrestore/buildが成功しました。
.NETテストは204件成功、Doomの既知の2件だけが失敗しました。追加HTTPスモークテスト4件も成功しました。
日英READMEのJSON例は実応答と項目・値・型が一致し、未実装APIの404も確認できました。

| 検証 | 成功 | 失敗 |
| --- | ---: | ---: |
| Demo .NET | 71 | 0 |
| Doom .NET | 133 | 2（既知） |
| README HTTP smoke | 4 | 0 |

既知の失敗は次の2件で、V2実施時とテスト名・成否・エラーメッセージが一致します。

- `DoomPrompt_UsesCanonicalAisthesisNoesisKrisisKinesisPanelMapping`
- `DoomWebGpuProvider_SeparatesRawFramebufferAndHudCompositeTargets`

どちらも`const HUD_UNIFORM_FLOAT_COUNT = 32`を探す既存Assert.Containsの失敗です。
既存テストの名前と成否をV2の記録と照合し、削除や新しい失敗がないことを確認しました。
期待値は変更していません。アプリ本体・契約・既存テストに差分がないことも確認しました。

機械可読の集計は`verified-results.json`、.NETテストは`results/*.trx`、HTTP試験は
`webapi-smoke.log`にあります。検証専用NuGetキャッシュを使用していることも確認しました。
Linux、GitHub CI、実GPUでの試験は今回実施していません。

証跡保存先: `D:\AIKernel\.codex_tmp\review-dm2-20261114`。
Demoは今回のブランチ、Doomはdevの隔離コピーを使用します。依存パッケージはキャッシュ済みの
0.1.3等を専用フィード・キャッシュに復元し、他のレビュー候補パッケージを混ぜていません。
既存のレビュー用ブランチとユーザーの未コミット変更は保持し、push、PR、devへの取り込み、
タグ、パッケージの公開は行いません。

## 次の項目と人間の判断

DM2はレビュー後のdevへの取り込み待ちです。ゲートウェイの仕様・実装は承認待ちのDM4で扱います。
次の候補はRH1（Lean CIの配置と検証）です。
