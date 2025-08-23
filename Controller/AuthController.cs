using System.Data;
using System.Security.Cryptography;
using System.Text;
using DotNetAPI.Data;
using DotNetAPI.Dots;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNetAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserForRegisterConfirmDto userForRegisterConfirmDto)
        {
            if (userForRegisterConfirmDto.passwordHash == userForRegisterConfirmDto.passwordConfirm)
            {
                string checkUserSql = "SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" + userForRegisterConfirmDto.Email + "' ";
                IEnumerable<string> userExists = _dapper.loadData<string>(checkUserSql);
                if (userExists.Count() == 0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    byte[] passwordHash = GetPasswordHash(userForRegisterConfirmDto.passwordHash, passwordSalt);

                    string sql = @"
                        INSERT INTO TutorialAppSchema.Auth (Email, PasswordHash, PasswordSalt)
                        VALUES(" +
                    "'" + userForRegisterConfirmDto.Email + "',@PasswordHash , @PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    SqlParameter passwordSaltParam = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                    passwordSaltParam.Value = passwordSalt;
                    SqlParameter passwordHashParam = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                    passwordHashParam.Value = passwordHash;

                    sqlParameters.Add(passwordSaltParam);
                    sqlParameters.Add(passwordHashParam);

                    if (_dapper.ExcuteSqlWithParamters(sql, sqlParameters))
                    {
                        string sqlCreate = @"
                    INSERT INTO TutorialAppSchema.USERS ([FirstName], [LastName], [Email], [Gender], [Active])
                        VALUES(" + "'" + userForRegisterConfirmDto.FirstName + "','" + userForRegisterConfirmDto.LastName +
                        "','" + userForRegisterConfirmDto.Email + "','" + userForRegisterConfirmDto.Gender + "', 1)";
                        if (_dapper.ExcuteSql(sqlCreate))
                        { return Ok(); }
                        throw new Exception("Failed to Create user details");
                    }
                    throw new Exception("Failed to register user");
                }
                throw new Exception("User already exists");
            }
            throw new Exception("Password and Confirm Password do not match");
        }
        [HttpGet("Login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            string sql = "SELECT * FROM TutorialAppSchema.Auth WHERE Email = '" + userForLoginDto.Email + "' ";
            UserForLoginConfirmDto user = _dapper.loadSingleData<UserForLoginConfirmDto>(sql);

            byte[] passwordHash = GetPasswordHash(userForLoginDto.password, user.passwordSalt);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.passwordHash[i])
                {
                    return StatusCode(401, "Invalid login attempt");
                }
            }

            return Ok("Login");
        }
        private byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltString = _config.GetValue<string>("AppSettings:PasswordSalt")
                    + Convert.ToBase64String(passwordSalt);
            byte[] passwordHash = KeyDerivation.Pbkdf2(
                        password: password,
                        salt: Encoding.ASCII.GetBytes(passwordSaltString),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8);
            return passwordHash;
        }

    }
}